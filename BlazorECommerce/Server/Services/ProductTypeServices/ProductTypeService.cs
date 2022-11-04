namespace BlazorECommerce.Server.Services.ProductTypeServices;

public record ProductTypeService(DatabaseContext DatabaseContext) : IProductTypeService
{
    public async Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync()
    {
        var productTypes = await DatabaseContext.ProductTypes.ToListAsync();
        return new ServiceResponse<List<ProductType>>(productTypes);
    }
}
