namespace BlazorECommerce.Client.Services.CartService;

public record CartService(ILocalStorageService LocalStorageService, HttpClient HttpClient) : ICartService
{
    private const string CartName = "cart";
    public event Action? OnChange;

    private List<CartItem> Cart { get; set; } = new();

    public async Task AddToCartAsync(CartItem cartItem)
    {
        await GetCartItemsAsync();

        var sameItem = await FindCartItemAsync(cartItem.ProductId, cartItem.ProductTypeId);

        if (sameItem is null)
        {
            Cart.Add(cartItem);
        }
        else
        {
            sameItem.Quantity += cartItem.Quantity;
        }

        await SaveCart(Cart);
    }

    private async Task<CartItem?> FindCartItemAsync(int productId, int productTypeId)
    {
        await GetCartItemsAsync();
        return Cart
            .Find(item => item.ProductId == productId &&
                          item.ProductTypeId == productTypeId);
    }

    private async Task SaveCart(List<CartItem> cart)
    {
        await LocalStorageService.SetItemAsync(CartName, cart);
        OnChange?.Invoke();
    }

    private async Task GetCartItemsAsync() =>
        Cart = await LocalStorageService.GetItemAsync<List<CartItem>>(CartName) ?? new List<CartItem>();

    public async Task<List<CartProductResponse>> GetCartProductsAsync()
    {
        await GetCartItemsAsync();
        var response = await HttpClient.PostAsJsonAsync("api/cart/products", Cart);
        var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
        return cartProducts?.Data ?? new List<CartProductResponse>();
    }

    public async Task RemoveProductAsync(int productId, int productTypeId)
    {
        await GetCartItemsAsync();
        if (Cart is not { Count: > 0 })
        {
            return;
        }

        var cartItem = await FindCartItemAsync(productId, productTypeId);
        if (cartItem is null)
        {
            return;
        }

        Cart.Remove(cartItem);
        await SaveCart(Cart);
    }

    public async Task UpdateQuantity(CartProductResponse product)
    {
        await GetCartItemsAsync();
        if (Cart is not { Count: > 0 })
        {
            return;
        }

        var cartItem = await FindCartItemAsync(product.ProductId, product.ProductTypeId);
        if (cartItem is null)
        {
            return;
        }

        cartItem.Quantity = product.Quantity;
        await SaveCart(Cart);
    }

    public async Task<int> ItemCount()
    {
        await GetCartItemsAsync();
        return Cart.Count;
    }
}
