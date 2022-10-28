namespace BlazorECommerce.Rcl.Components;

public partial class ProductList : IDisposable
{
    protected override void OnInitialized() => ProductService.ProductsChanged += StateHasChanged;

    public void Dispose() => ProductService.ProductsChanged -= StateHasChanged;
}
