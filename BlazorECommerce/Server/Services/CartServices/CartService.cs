using BlazorECommerce.Shared.Models;

namespace BlazorECommerce.Server.Services.CartServices;

public record CartService(DatabaseContext DatabaseContext, IHttpContextAccessor HttpContextAccessor) : ICartService
{
    public Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems) =>
        Task.FromResult(new ServiceResponse<List<CartProductResponse>>
        (
            new List<CartProductResponse>(cartItems.Select(cartItem => (
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
        ));

    public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItemsAsync(List<CartItem> cartItems)
    {
        var userId = GetUserId();
        cartItems.ForEach(cartItem => cartItem.UserId = userId);
        DatabaseContext.CartItems.AddRange(cartItems);
        await DatabaseContext.SaveChangesAsync();

        return await GetDbCartProductsAsync();
    }

    public async Task<ServiceResponse<int>> GetCartItemsCountAsync()
    {
        var userId = GetUserId();
        var count = await DatabaseContext.CartItems.CountAsync(cartItem => cartItem.UserId == userId);
        return new ServiceResponse<int>(count);
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProductsAsync()
    {
        var userId = GetUserId();
        return await GetCartProductsAsync(await DatabaseContext.CartItems.Where(cartItem => cartItem.UserId == userId)
            .ToListAsync());
    }

    private int GetUserId()
    {
        if (HttpContextAccessor is not { HttpContext.User: not null })
        {
            return 0;
        }

        var (userIdFound, userId) = SharedMethods.GetUserIdFromClaimsPrincipal(HttpContextAccessor.HttpContext.User);
        return !userIdFound ? 0 : userId;
    }
}
