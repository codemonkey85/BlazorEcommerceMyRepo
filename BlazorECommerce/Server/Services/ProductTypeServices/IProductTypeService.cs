namespace BlazorECommerce.Server.Services.ProductTypeServices;

public interface IProductTypeService
{
    Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync();
}
