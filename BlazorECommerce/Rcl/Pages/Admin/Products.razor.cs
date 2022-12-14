namespace BlazorECommerce.Rcl.Pages.Admin;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Products : IDisposable
{
    protected override async Task OnInitializedAsync()
    {
        await ProductService.GetAdminProductsAsync();
        ProductService.ProductsChanged += StateHasChanged;
    }

    public void Dispose() => ProductService.ProductsChanged -= StateHasChanged;

    private void EditProduct(int productId) => NavigationManager.NavigateTo($"admin/{nameof(Product)}/{productId}");

    private void CreateProduct() => NavigationManager.NavigateTo($"admin/{nameof(Product)}");
}
