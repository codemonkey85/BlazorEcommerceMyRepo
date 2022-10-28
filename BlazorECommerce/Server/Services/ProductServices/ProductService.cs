namespace BlazorECommerce.Server.Services.ProductServices;

public record ProductService(DatabaseContext DatabaseContext) : IProductService
{
    public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
    {
        var data = await DatabaseContext.Products.ToListAsync();
        return new ServiceResponse<List<Product>> { Data = data };
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId) =>
        await DatabaseContext.Products.FindAsync(productId) switch
        {
            null => new ServiceResponse<Product> { Success = false, Message = $"{nameof(Product)} not found." },
            var product => new ServiceResponse<Product> { Data = product }
        };
}
