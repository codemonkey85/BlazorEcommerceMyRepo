namespace BlazorECommerce.Server.Services.CartServices;

public record CartService(DatabaseContext DatabaseContext) : ICartService
{
    public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems)
    {
        var result = new ServiceResponse<List<CartProductResponse>>
        {
            Data = new List<CartProductResponse>()
        };

        foreach (var cartItem in cartItems)
        {
            var product = await DatabaseContext.Products
                .Where(product => product.Id == cartItem.ProductId)
                .FirstOrDefaultAsync();

            if (product is null)
            {
                continue;
            }

            var productVariant = await DatabaseContext.ProductVariants
                .Include(variant => variant.ProductType)
                .Where(variant => variant.ProductId == cartItem.ProductId && variant.ProductTypeId == cartItem.ProductTypeId)
                .FirstOrDefaultAsync();

            if (productVariant is null)
            {
                continue;
            }

            var cartProduct = new CartProductResponse
            {
                ProductId = product.Id,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Price = productVariant.Price,
                ProductType = productVariant.ProductType.Name,
                ProductTypeId = productVariant.ProductTypeId,
                Quantity = cartItem.Quantity,
            };

            result.Data.Add(cartProduct);
        }

        return result;
    }
}
