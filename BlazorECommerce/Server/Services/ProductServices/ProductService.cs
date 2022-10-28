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

    public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await DatabaseContext.Products
                .Include(product => product.Variants)
                .Where(product => product.Category != null && product.Category.Url == categoryUrl)
                .ToListAsync()
        };
        return response;
    }
}
