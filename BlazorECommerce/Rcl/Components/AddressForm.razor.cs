namespace BlazorECommerce.Rcl.Components;

public partial class AddressForm
{
    private Address? address;
    private bool editAddress;

    protected override async Task OnInitializedAsync() => address = await AddressService.GetAddressAsync();

    private async Task SubmitAddress()
    {
        if (address is null)
        {
            return;
        }

        address = await AddressService.AddOrUpdateAddressAsync(address);
        editAddress = false;
    }

    private void InitAddress()
    {
        address = new Address();
        editAddress = true;
    }

    private void EditAddress() => editAddress = true;
}
