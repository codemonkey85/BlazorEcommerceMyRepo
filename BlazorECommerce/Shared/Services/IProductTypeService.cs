namespace BlazorECommerce.Shared.Services;

public interface IProductTypeService
{
    public event Action OnChange;

    public List<ProductType> ProductTypes { get; set; }

    Task GetProductTypesAsync();

    Task AddProductTypeAsync(ProductType productType);

    Task UpdateProductTypeAsync(ProductType productType);

    ProductType CreateNewProductType();
}
