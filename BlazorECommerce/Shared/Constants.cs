namespace BlazorECommerce.Shared;

public static class Constants
{
    public const string Add = "Add";
    public const string Admin = "Admin";
    public const string Api = "Api";
    public const string Auth = "Auth";
    public const string Cart = "Cart";
    public const string ChangePassword = "ChangePassword";
    public const string Checkout = "Checkout";
    public const string Count = "Count";
    public const string Featured = "Featured";
    public const string Login = "Login";
    public const string Payment = "Payment";
    public const string Products = "Products";
    public const string Register = "Register";
    public const string Search = "Search";
    public const string SearchSuggestions = "searchSuggestions";
    public const string UpdateQuantity = "UpdateQuantity";

    public const string Address = nameof(Models.Address);
    public const string Category = nameof(Models.Category);
    public const string Order = nameof(Models.Order);
    public const string Product = nameof(Models.Product);
    public const string ProductType = nameof(Models.ProductType);

    private const string AuthApi = $"{Api}/{Auth}";
    private const string PaymentApi = $"{Api}/{Payment}";
    private const string ProductTypeApi = $"{Api}/{ProductType}";

    public const string AddressApi = $"{Api}/{Address}";
    public const string AdminCategoryApi = $"{CategoryApi}/{Admin}";
    public const string AdminProductApi = $"{ProductApi}/{Admin}";
    public const string AdminProductTypeApi = $"{ProductTypeApi}/{Admin}";
    public const string AuthChangePasswordApi = $"{AuthApi}/{ChangePassword}";
    public const string AuthLoginApi = $"{AuthApi}/{Login}";
    public const string AuthRegisterApi = $"{AuthApi}/{Register}";
    public const string CartAddApi = $"{CartApi}/{Add}";
    public const string CartApi = $"{Api}/{Cart}";
    public const string CartCountApi = $"{CartApi}/{Count}";
    public const string CartProductsApi = $"{CartApi}/{Products}";
    public const string CartUpdateQuantityApi = $"{CartApi}/{UpdateQuantity}";
    public const string CategoryApi = $"{Api}/{Category}";
    public const string OrderApi = $"{Api}/{Order}";
    public const string PaymentCheckoutApi = $"{PaymentApi}/{Checkout}";
    public const string ProductApi = $"{Api}/{Product}";
    public const string ProductCategoryApi = $"{ProductApi}/{Category}";
    public const string ProductFeaturedApi = $"{ProductApi}/{Featured}";
    public const string ProductSearchApi = $"{ProductApi}/{Search}";
    public const string ProductSearchSuggestionsApi = $"{ProductApi}/{SearchSuggestions}";
}
