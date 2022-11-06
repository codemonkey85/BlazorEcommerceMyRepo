namespace BlazorECommerce.Rcl.Components;

public partial class ProductFirstImage
{
    [Parameter, EditorRequired] public Product? Product { get; set; }

    [Parameter] public string CssClass { get; set; } = string.Empty;
}