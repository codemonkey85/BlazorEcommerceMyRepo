namespace BlazorECommerce.Rcl.Components;

public partial class ProductList
{
    private static List<Product> _products = new();

    protected override async Task OnInitializedAsync()
    {
        await ProductService.GetProductsAsync();
        _products = ProductService.Products;
    }
}
