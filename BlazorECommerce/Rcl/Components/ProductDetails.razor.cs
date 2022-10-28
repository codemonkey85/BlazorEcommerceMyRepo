namespace BlazorECommerce.Rcl.Components;

public partial class ProductDetails
{
    private Product? _product = null;
    private string _message = string.Empty;

    [Parameter] public int Id { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        _message = "Loading Product...";
        var result = await ProductService.GetProductAsync(Id);
        if (result.Success)
        {
            _product = result.Data;
        }
        else
        {
            _message = result.Message;
        }
    }
}
