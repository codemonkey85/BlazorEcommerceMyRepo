namespace BlazorECommerce.Server.Services.ProductTypeServices;

public interface IProductTypeService
{
    Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync();

    Task<ServiceResponse<List<ProductType>>> AddProductTypeAsync(ProductType productType);

    Task<ServiceResponse<List<ProductType>>> UpdateProductTypeAsync(ProductType productType);
}
