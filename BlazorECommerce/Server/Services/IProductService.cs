namespace BlazorECommerce.Server.Services;

public interface IProductService
{
    Task<ServiceResponse<List<Product>>> GetProductListAsync();
}
