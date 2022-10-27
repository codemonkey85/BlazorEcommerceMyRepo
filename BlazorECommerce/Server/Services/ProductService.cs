namespace BlazorECommerce.Server.Services;

public record ProductService(DatabaseContext DatabaseContext) : IProductService
{
    public async Task<ServiceResponse<List<Product>>> GetProductListAsync()
    {
        var data = await DatabaseContext.Products.ToListAsync();
        return new ServiceResponse<List<Product>>
        {
            Data = data
        };
    }
}
