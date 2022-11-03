namespace BlazorECommerce.Server.Services.AddressServices;

public interface IAddressService
{
    Task<ServiceResponse<Address?>> GetAddressAsync();

    Task<ServiceResponse<Address>> AddOrUpdateAddressAsync(Address address);
}
