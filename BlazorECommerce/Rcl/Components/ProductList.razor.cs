namespace BlazorECommerce.Rcl.Components;

public partial class ProductList
{
    protected override async Task OnInitializedAsync() => await ProductService.GetProductsAsync();
}
