namespace BlazorECommerce.Rcl.Components;

public partial class ProductList
{
    private static List<Product> _products = new();

    protected override async Task OnInitializedAsync()
    {
        var result = await HttpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/{nameof(Product)}");
        if (result is { Data: not null })
        {
            _products = result.Data;
        }
    }
}
