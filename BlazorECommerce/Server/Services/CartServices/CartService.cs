namespace BlazorECommerce.Server.Services.CartServices;

public record CartService(DatabaseContext DatabaseContext) : ICartService
{
    public Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems) =>
        Task.FromResult(new ServiceResponse<List<CartProductResponse>>
        {
            Data = new List<CartProductResponse>(cartItems.Select(cartItem => (
                        from products in DatabaseContext.Products
                        join productVariants in DatabaseContext.ProductVariants.Include(v => v.ProductType) on
                            new { ProductId = products.Id, cartItem.ProductTypeId }
                            equals new { productVariants.ProductId, productVariants.ProductTypeId }
                        where products.Id == cartItem.ProductId &&
                              productVariants.ProductTypeId == cartItem.ProductTypeId
                        select new CartProductResponse
                        {
                            ProductId = products.Id,
                            Title = products.Title,
                            ImageUrl = products.ImageUrl,
                            Price = productVariants.Price,
                            ProductType = productVariants.ProductType.Name,
                            ProductTypeId = productVariants.ProductTypeId,
                            Quantity = cartItem.Quantity,
                        }
                    ).First()
                )
                .OrderBy(c => c.ProductId)
                .ThenBy(c => c.ProductTypeId)
            )
        });
}
