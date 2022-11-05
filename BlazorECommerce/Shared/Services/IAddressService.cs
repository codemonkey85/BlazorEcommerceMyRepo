namespace BlazorECommerce.Shared.Services;

public interface IAddressService
{
    Task<Address?> GetAddressAsync();

    Task<Address?> AddOrUpdateAddressAsync(Address address);
}
