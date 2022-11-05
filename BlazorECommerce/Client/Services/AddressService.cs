namespace BlazorECommerce.Client.Services;

public record AddressService(HttpClient HttpClient) : IAddressService
{
    public async Task<Address?> GetAddressAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<ServiceResponse<Address>>(Constants.AddressApi);
        return response?.Data;
    }

    public async Task<Address?> AddOrUpdateAddressAsync(Address address)
    {
        var response = await HttpClient.PostAsJsonAsync(Constants.AddressApi, address);
        return response.Content.ReadFromJsonAsync<ServiceResponse<Address?>>().Result?.Data;
    }
}
