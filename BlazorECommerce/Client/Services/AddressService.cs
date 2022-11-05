namespace BlazorECommerce.Client.Services;

public record AddressService(HttpClient HttpClient) : IAddressService
{
    public async Task<Address?> GetAddressAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<ServiceResponse<Address>>($"api/{nameof(Address)}");
        return response?.Data;
    }

    public async Task<Address?> AddOrUpdateAddressAsync(Address address)
    {
        var response = await HttpClient.PostAsJsonAsync($"api/{nameof(Address)}", address);
        return response.Content.ReadFromJsonAsync<ServiceResponse<Address?>>().Result?.Data;
    }
}
