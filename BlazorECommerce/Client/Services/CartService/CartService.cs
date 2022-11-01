namespace BlazorECommerce.Client.Services.CartService;

public record CartService(ILocalStorageService LocalStorageService, HttpClient HttpClient,
    AuthenticationStateProvider AuthStateProvider) : ICartService
{
    private const string CartName = "cart";
    public event Action? OnChange;

    private List<CartItem> Cart { get; set; } = new();

    public async Task AddToCartAsync(CartItem cartItem)
    {
        if (await IsUserAuthenticatedAsync())
        {
            Console.WriteLine("User is authenticated.");
        }
        else
        {
            Console.WriteLine("User is not authenticated.");
        }

        await GetCartItemsFromLocalStorageAsync();

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

    private async Task<bool> IsUserAuthenticatedAsync() => await AuthStateProvider.GetAuthenticationStateAsync() is
        { User.Identity.IsAuthenticated: true };

    private async Task<CartItem?> FindCartItemAsync(int productId, int productTypeId)
    {
        await GetCartItemsFromLocalStorageAsync();
        return Cart
            .Find(item => item.ProductId == productId &&
                          item.ProductTypeId == productTypeId);
    }

    private async Task SaveCart(List<CartItem> cart)
    {
        await LocalStorageService.SetItemAsync(CartName, cart);
        await GetCartItemsCountAsync();
        OnChange?.Invoke();
    }

    private async Task GetCartItemsFromLocalStorageAsync() =>
        Cart = await LocalStorageService.GetItemAsync<List<CartItem>>(CartName) ?? new List<CartItem>();

    public async Task<List<CartProductResponse>> GetCartProductsAsync()
    {
        if (await IsUserAuthenticatedAsync())
        {
            var response = await HttpClient.GetFromJsonAsync<ServiceResponse<List<CartProductResponse>>>("api/cart");
            return response?.Data ?? new List<CartProductResponse>();
        }
        else
        {
            await GetCartItemsFromLocalStorageAsync();
            if (Cart is not { Count: > 0 })
            {
                return new List<CartProductResponse>();
            }

            var response = await HttpClient.PostAsJsonAsync("api/cart/products", Cart);
            var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
            return cartProducts?.Data ?? new List<CartProductResponse>();
        }
    }

    public async Task RemoveProductAsync(int productId, int productTypeId)
    {
        await GetCartItemsFromLocalStorageAsync();
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
        await GetCartItemsFromLocalStorageAsync();
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
        await GetCartItemsFromLocalStorageAsync();
        return Cart.Count;
    }

    public async Task StoreCartItemsAsync(bool emptyLocalCart = false)
    {
        await GetCartItemsFromLocalStorageAsync();
        if (Cart is not { Count: > 0 })
        {
            return;
        }

        await HttpClient.PostAsJsonAsync("api/cart", Cart);

        if (emptyLocalCart)
        {
            await LocalStorageService.RemoveItemAsync(CartName);
        }
    }

    public async Task GetCartItemsCountAsync()
    {
        int count;
        if (await IsUserAuthenticatedAsync())
        {
            var result = await HttpClient.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
            count = result?.Data ?? 0;
        }
        else
        {
            await GetCartItemsFromLocalStorageAsync();
            count = Cart.Count;
        }

        await LocalStorageService.SetItemAsync("cartItemsCount", count);
        OnChange?.Invoke();
    }
}
