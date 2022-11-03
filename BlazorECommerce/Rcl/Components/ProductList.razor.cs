namespace BlazorECommerce.Rcl.Components;

public partial class ProductList : IDisposable
{
    protected override void OnInitialized() => ProductService.ProductsChanged += StateHasChanged;

    public void Dispose() => ProductService.ProductsChanged -= StateHasChanged;

    private static string GetPriceText(Product product) => product.Variants switch
    {
        null or { Count: 0 } => string.Empty,
        { Count: 1 } => $"{product.Variants.FirstOrDefault()?.Price:C}",
        { Count: > 1 } => $"Starting at {product.Variants.MinBy(variant => variant.Price)?.Price:C}"
    };
}
