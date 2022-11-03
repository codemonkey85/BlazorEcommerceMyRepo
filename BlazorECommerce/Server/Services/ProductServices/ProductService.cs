namespace BlazorECommerce.Server.Services.ProductServices;

public record ProductService(DatabaseContext DatabaseContext) : IProductService
{
    public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
    {
        var data = await DatabaseContext.Products
            .Include(product => product.Variants)
            .ToListAsync();
        return new ServiceResponse<List<Product>>(data);
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId) =>
        await DatabaseContext.Products
                .Include(product => product.Variants)
                .ThenInclude(variant => variant.ProductType)
                .FirstOrDefaultAsync(product => product.Id == productId) switch
            {
                null => new ServiceResponse<Product> { Success = false, Message = $"{nameof(Product)} not found." },
                var product => new ServiceResponse<Product>(product)
            };

    public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl) => new
    (
        await DatabaseContext.Products
            .Include(product => product.Variants)
            .Where(product => product.Category != null && product.Category.Url == categoryUrl)
            .ToListAsync()
    );

    private IQueryable<Product> FindProductsBySearchStringAsync(string searchText) =>
        DatabaseContext.Products
            .Include(product => product.Variants)
            .Where(product => product.Title.ToLower().Contains(searchText.ToLower()) ||
                              product.Description.ToLower().Contains(searchText.ToLower()));

    public async Task<ServiceResponse<ProductSearchResult>> SearchProductsAsync(string searchText, int page)
    {
        const int pageSize = 2;
        var productsFound = await FindProductsBySearchStringAsync(searchText).ToListAsync();
        var pageCount = (int)Math.Ceiling(productsFound.Count / (float)pageSize);

        var products = productsFound
            .OrderBy(product => product.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new ServiceResponse<ProductSearchResult>
        (
            new ProductSearchResult { Products = products, CurrentPage = page, Pages = pageCount }
        );
    }

    public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText) => new
    (
        await FindProductsBySearchStringAsync(searchText)
            .Select(product => product.Title)
            .ToListAsync()
    );

    public async Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync() => new
    (
        await DatabaseContext.Products
            .Include(product => product.Variants)
            .Where(product => product.Featured)
            .ToListAsync()
    );
}
