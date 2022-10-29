namespace BlazorECommerce.Server.Services.ProductServices;

public record ProductService(DatabaseContext DatabaseContext) : IProductService
{
    public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
    {
        var data = await DatabaseContext.Products
            .Include(product => product.Variants)
            .ToListAsync();
        return new ServiceResponse<List<Product>> { Data = data };
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId) =>
        await DatabaseContext.Products
                .Include(product => product.Variants)
                .ThenInclude(variant => variant.ProductType)
                .FirstOrDefaultAsync(product => product.Id == productId) switch
        {
            null => new ServiceResponse<Product> { Success = false, Message = $"{nameof(Product)} not found." },
            var product => new ServiceResponse<Product> { Data = product }
        };

    public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl) => new()
    {
        Data = await DatabaseContext.Products
            .Include(product => product.Variants)
            .Where(product => product.Category != null && product.Category.Url == categoryUrl)
            .ToListAsync()
    };

    private IQueryable<Product> FindProductsBySearchStringAsync(string searchText) =>
        DatabaseContext.Products
                .Include(product => product.Variants)
                .Where(product => product.Title.ToLower().Contains(searchText.ToLower()) ||
                                  product.Description.ToLower().Contains(searchText.ToLower()));
    public async Task<ServiceResponse<List<Product>>> SearchProductsAsync(string searchText) => new()
    {
        Data = await FindProductsBySearchStringAsync(searchText).ToListAsync()
    };


    public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText) => new()
    {
        Data = await FindProductsBySearchStringAsync(searchText).Select(product => product.Title).ToListAsync()
    };
}
