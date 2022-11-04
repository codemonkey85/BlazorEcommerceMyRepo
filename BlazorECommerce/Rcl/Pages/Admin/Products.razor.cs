namespace BlazorECommerce.Rcl.Pages.Admin;

public partial class Products : IDisposable
{
    protected override async Task OnInitializedAsync()
    {
        await ProductService.GetAdminProductsAsync();
        ProductService.ProductsChanged += StateHasChanged;
    }

    public void Dispose() => ProductService.ProductsChanged -= StateHasChanged;

    private void EditProduct(int productId) => NavigationManager.NavigateTo($"admin/product/{productId}");

    private void CreateProduct() => NavigationManager.NavigateTo($"admin/product");
}
