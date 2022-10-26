namespace BlazorECommerce.Rcl.Components;

public partial class ProductList
{
    private static List<Product> _products = new();

    protected override async Task OnInitializedAsync() =>
        _products = await HttpClient.GetFromJsonAsync<List<Product>>($"api/{nameof(Product)}") ?? new List<Product>();
}
