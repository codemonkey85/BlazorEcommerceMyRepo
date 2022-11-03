namespace BlazorECommerce.Shared.Services.AddressServices;

public interface IAddressService
{
    Task<Address?> GetAddressAsync();

    Task<Address?> AddOrUpdateAddressAsync(Address address);
}
