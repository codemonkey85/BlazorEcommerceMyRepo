namespace BlazorECommerce.Rcl.Components;

public partial class ProductImagesCarousel
{
    [Parameter, EditorRequired] public Product? Product { get; set; }
}
