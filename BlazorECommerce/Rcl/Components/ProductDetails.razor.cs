namespace BlazorECommerce.Rcl.Components;

public partial class ProductDetails
{
    private Product? _product = null;
    private string _message = string.Empty;
    private int _currentTypeId = 1;

    [Parameter] public int Id { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        _message = "Loading Product...";
        var result = await ProductService.GetProductAsync(Id);
        if (result.Success)
        {
            _product = result.Data;
            if (_product is { Variants.Count: > 0 })
            {
                _currentTypeId = _product.Variants[0].ProductTypeId;
            }
        }
        else
        {
            _message = result.Message;
        }
    }

    private ProductVariant? SelectedVariant =>
        _product?.Variants.FirstOrDefault(v => v.ProductTypeId == _currentTypeId);
}
