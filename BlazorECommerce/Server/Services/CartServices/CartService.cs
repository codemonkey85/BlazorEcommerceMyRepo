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
            var cartProduct = await (
                from products in DatabaseContext.Products
                join productVariants in DatabaseContext.ProductVariants.Include(v => v.ProductType) on new
                { ProductId = products.Id, cartItem.ProductTypeId } equals new
                { productVariants.ProductId, productVariants.ProductTypeId }
                where products.Id == cartItem.ProductId
                select new CartProductResponse
                {
                    ProductId = products.Id,
                    Title = products.Title,
                    ImageUrl = products.ImageUrl,
                    Price = productVariants.Price,
                    ProductType = productVariants.ProductType.Name,
                    ProductTypeId = productVariants.ProductTypeId,
                    Quantity = cartItem.Quantity,
                }).FirstOrDefaultAsync();

            if (cartProduct is not null)
            {
                result.Data.Add(cartProduct);
            }
        }

        return result;
    }
}
