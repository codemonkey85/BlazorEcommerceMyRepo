namespace BlazorECommerce.Rcl.Components;

public partial class ProductFirstImage
{
    [Parameter] public Product? Product { get; set; }

    [Parameter] public CartProductResponse? CartProductResponse { get; set; }

    [Parameter] public string CssClass { get; set; } = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        if (Product is null && CartProductResponse is not null)
        {
            var response = await ProductService.GetProductAsync(CartProductResponse.ProductId);
            Product = response.Data;
        }
    }
}
