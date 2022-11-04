namespace BlazorECommerce.Shared.Services.ProductTypeServices;

public interface IProductTypeService
{
    public event Action OnChange;

    public List<ProductType> ProductTypes { get; set; }

    Task GetProductTypesAsync();

    Task AddProductTypeAsync(ProductType productType);

    Task UpdateProductTypeAsync(ProductType productType);

    ProductType CreateNewProductType();
}
