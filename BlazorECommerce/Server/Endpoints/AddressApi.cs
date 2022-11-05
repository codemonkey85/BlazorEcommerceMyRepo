namespace BlazorECommerce.Server.Endpoints;

public static class AddressApi
{
    public static IEndpointRouteBuilder MapAddressApi(this IEndpointRouteBuilder apiGroup)
    {
        var addressGroup = apiGroup.MapGroup(nameof(Address))
            .RequireAuthorization();

        addressGroup.MapGet(string.Empty, GetAddressAsync);
        addressGroup.MapPost(string.Empty, AddOrUpdateAddressAsync);

        return apiGroup;
    }

    private static async Task<ServiceResponse<Address?>> GetAddressAsync(IAddressService addressService) =>
        await addressService.GetAddressAsync();

    private static async Task<ServiceResponse<Address>> AddOrUpdateAddressAsync(IAddressService addressService,
        Address address) =>
        await addressService.AddOrUpdateAddressAsync(address);
}
