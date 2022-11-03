namespace BlazorECommerce.Rcl.Components;

public partial class ProductDetails
{
    private Product? product = null;
    private string message = string.Empty;
    private int currentTypeId = 1;

    [Parameter] public int ProductId { get; set; }

    [Parameter] public int? ProductTypeId { get; set; } = 1;

    protected override async Task OnParametersSetAsync()
    {
        message = "Loading Product...";
        var results = await ProductService.GetProductAsync(ProductId);
        if (results.Success)
        {
            product = results.Data;
            if (product is { Variants.Count: > 0 })
            {
                currentTypeId = product.Variants[0].ProductTypeId;
            }
        }
        else
        {
            message = results.Message;
        }

        if (ProductTypeId is > 0)
        {
            currentTypeId = ProductTypeId ?? 1;
        }
    }

    private ProductVariant? SelectedVariant =>
        product?.Variants.FirstOrDefault(v => v.ProductTypeId == currentTypeId);

    private async Task AddToCartAsync(int quantity = 1)
    {
        if (SelectedVariant is null)
        {
            return;
        }

        var cartItem = new CartItem
        {
            ProductId = SelectedVariant.ProductId,
            ProductTypeId = SelectedVariant.ProductTypeId,
            Quantity = quantity < 1 ? 1 : quantity
        };

        await CartService.AddToCartAsync(cartItem);
    }
}
