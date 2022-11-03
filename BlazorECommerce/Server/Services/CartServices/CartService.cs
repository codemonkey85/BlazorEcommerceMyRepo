namespace BlazorECommerce.Server.Services.CartServices;

public record CartService(DatabaseContext DatabaseContext, IAuthService AuthService) : ICartService
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
                            Quantity = cartItem.Quantity
                        }
                    ).First()
                )
                .OrderBy(c => c.ProductId)
                .ThenBy(c => c.ProductTypeId)
            )
        ));

    public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItemsAsync(List<CartItem> cartItems)
    {
        var userId = AuthService.GetUserId();
        cartItems.ForEach(cartItem => cartItem.UserId = userId);
        DatabaseContext.CartItems.AddRange(cartItems);
        await DatabaseContext.SaveChangesAsync();

        return await GetDbCartProductsAsync(userId);
    }

    public async Task<ServiceResponse<int>> GetCartItemsCountAsync()
    {
        var userId = AuthService.GetUserId();
        var count = await DatabaseContext.CartItems.CountAsync(cartItem => cartItem.UserId == userId);
        return new ServiceResponse<int>(count);
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProductsAsync(int? userId = null)
    {
        userId ??= AuthService.GetUserId();
        return await GetCartProductsAsync(await DatabaseContext.CartItems
            .Where(cartItem => cartItem.UserId == userId)
            .ToListAsync());
    }

    public async Task<ServiceResponse<bool>> AddToCartAsync(CartItem cartItem)
    {
        cartItem.UserId = AuthService.GetUserId();
        var sameItem = await FindCartItemAsync(cartItem.UserId, cartItem.ProductId, cartItem.ProductTypeId);
        if (sameItem is null)
        {
            DatabaseContext.CartItems.Add(cartItem);
        }
        else
        {
            sameItem.Quantity += cartItem.Quantity;
        }

        await DatabaseContext.SaveChangesAsync();
        return new ServiceResponse<bool>(true);
    }

    public async Task<ServiceResponse<bool>> UpdateQuantityAsync(CartItem cartItem)
    {
        var userId = AuthService.GetUserId();
        var sameItem = await FindCartItemAsync(userId, cartItem.ProductId, cartItem.ProductTypeId);
        if (sameItem is null)
        {
            return new ServiceResponse<bool>(false) { Message = "Cart item does not exist.", Success = false };
        }

        sameItem.Quantity = cartItem.Quantity;

        await DatabaseContext.SaveChangesAsync();
        return new ServiceResponse<bool>(true);
    }

    public async Task<ServiceResponse<bool>> RemoveItemFromCartAsync(int productId, int productTypeId)
    {
        var userId = AuthService.GetUserId();
        var sameItem = await FindCartItemAsync(userId, productId, productTypeId);
        if (sameItem is null)
        {
            return new ServiceResponse<bool>(false) { Message = "Cart item does not exist.", Success = false };
        }

        DatabaseContext.CartItems.Remove(sameItem);

        await DatabaseContext.SaveChangesAsync();
        return new ServiceResponse<bool>(true);
    }

    private async Task<CartItem?> FindCartItemAsync(int userId, int productId, int productTypeId) =>
        await DatabaseContext.CartItems
            .FirstOrDefaultAsync(item =>
                item.UserId == userId && item.ProductId == productId && item.ProductTypeId == productTypeId);
}
