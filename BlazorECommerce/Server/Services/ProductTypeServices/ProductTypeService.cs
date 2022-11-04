namespace BlazorECommerce.Server.Services.ProductTypeServices;

public record ProductTypeService(DatabaseContext DatabaseContext) : IProductTypeService
{
    public async Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync()
    {
        var productTypes = await DatabaseContext.ProductTypes.ToListAsync();
        return new ServiceResponse<List<ProductType>>(productTypes);
    }

    public async Task<ServiceResponse<List<ProductType>>> AddProductTypeAsync(ProductType productType)
    {
        productType.IsEditing = productType.IsNew = false;
        DatabaseContext.ProductTypes.Add(productType);
        await DatabaseContext.SaveChangesAsync();
        return await GetProductTypesAsync();
    }

    public async Task<ServiceResponse<List<ProductType>>> UpdateProductTypeAsync(ProductType productType)
    {
        var dbProductType = await DatabaseContext.ProductTypes.FindAsync(productType.Id);
        if (dbProductType is null)
        {
            return new ServiceResponse<List<ProductType>> { Success = false, Message = "Product Type not found." };
        }

        dbProductType.Name = productType.Name;

        await DatabaseContext.SaveChangesAsync();
        return await GetProductTypesAsync();
    }
}
