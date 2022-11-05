namespace BlazorECommerce.Shared;

public static class Constants
{
    public const string Api = "Api";
    public const string Admin = "Admin";
    public const string Address = nameof(Models.Address);
    public const string Auth = "Auth";
    public const string Cart = "Cart";
    public const string Category = nameof(Models.Category);
    public const string Order = nameof(Models.Order);
    public const string Product = nameof(Models.Product);
    public const string ProductType = nameof(Models.ProductType);
    public const string Payment = "Payment";

    public const string AddressApi = $"{Api}/{Address}";
    public const string AdminAddressApi = $"{AddressApi}/{Admin}";

    public const string AuthApi = $"{Api}/{Auth}";

    public const string CartApi = $"{Api}/{Cart}";

    public const string CategoryApi = $"{Api}/{Category}";
    public const string AdminCategoryApi = $"{CategoryApi}/{Admin}";

    public const string PaymentApi = $"{Api}/{Payment}";

    public const string OrderApi = $"{Api}/{Order}";

    public const string ProductApi = $"{Api}/{Product}";
    public const string AdminProductApi = $"{ProductApi}/{Admin}";

    public const string ProductTypeApi = $"{Api}/{ProductType}";
    public const string AdminProductTypeApi = $"{ProductTypeApi}/{Admin}";
}
