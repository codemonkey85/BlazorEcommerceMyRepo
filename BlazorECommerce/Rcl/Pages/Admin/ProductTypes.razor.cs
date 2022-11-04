namespace BlazorECommerce.Rcl.Pages.Admin;

public partial class ProductTypes : IDisposable
{
    private ProductType? editingProductType;

    protected override async Task OnInitializedAsync()
    {
        await ProductTypeService.GetProductTypesAsync();
        ProductTypeService.OnChange += StateHasChanged;
    }

    public void Dispose() => ProductTypeService.OnChange -= StateHasChanged;

    private void EditProductType(ProductType productType)
    {
        productType.IsEditing = true;
        editingProductType = productType;
    }

    private void CreateNewProductType() => editingProductType = ProductTypeService.CreateNewProductType();

    private async Task UpdateProductTypeAsync()
    {
        var productTypeTask = editingProductType switch
        {
            null => null,
            { IsNew: true } => ProductTypeService.AddProductTypeAsync(editingProductType),
            { IsNew: false } => ProductTypeService.UpdateProductTypeAsync(editingProductType)
        };

        if (productTypeTask is not null)
        {
            await productTypeTask;
        }

        editingProductType = new ProductType();
    }

    private async Task CancelEditingAsync()
    {
        editingProductType = new ProductType();
        await ProductTypeService.GetProductTypesAsync();
    }
}
