namespace BlazorECommerce.Rcl.Components;

public partial class ProductImages
{
    [Parameter, EditorRequired] public Product? Product { get; set; }

    private void RemoveImage(int productId)
    {
        if (Product is not { Images.Count: > 0 })
        {
            return;
        }

        var image = Product.Images.FirstOrDefault(i => i.Id == productId);
        if (image is not null)
        {
            Product.Images.Remove(image);
        }
    }
}
