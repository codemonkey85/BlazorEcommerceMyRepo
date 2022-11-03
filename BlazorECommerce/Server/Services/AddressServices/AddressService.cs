namespace BlazorECommerce.Server.Services.AddressServices;

public record AddressService(DatabaseContext DatabaseContext, IAuthService AuthService) : IAddressService
{
    public async Task<ServiceResponse<Address?>> GetAddressAsync()
    {
        var userId = AuthService.GetUserId();
        var address = await DatabaseContext.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);
        return new ServiceResponse<Address?>(address);
    }

    public async Task<ServiceResponse<Address>> AddOrUpdateAddressAsync(Address address)
    {
        var response = new ServiceResponse<Address>();
        var dbAddress = (await GetAddressAsync()).Data;
        var userId = AuthService.GetUserId();
        if (dbAddress is null)
        {
            address.UserId = userId;
            DatabaseContext.Addresses.Add(address);
            response.Data = address;
        }
        else
        {
            dbAddress.FirstName = address.FirstName;
            dbAddress.LastName = address.LastName;
            dbAddress.Street = address.Street;
            dbAddress.City = address.City;
            dbAddress.State = address.State;
            dbAddress.Zip = address.Zip;
            dbAddress.Country = address.Country;
            response.Data = dbAddress;
        }

        await DatabaseContext.SaveChangesAsync();
        return response;
    }
}
