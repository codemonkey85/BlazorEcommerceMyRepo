namespace BlazorECommerce.Rcl.Pages.Admin;

public partial class EditProduct
{
    [Parameter] public int ProductId { get; set; }

    private Product product = new();
    private bool loading = true;
    private string btnText = string.Empty;
    private string message = "loading";

    protected override async Task OnInitializedAsync()
    {
        await ProductTypeService.GetProductTypesAsync();
        await CategoryService.GetAdminCategoriesAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (ProductId == 0)
        {
            product = new Product { IsNew = true };
            btnText = "Create Product";
        }
        else
        {
            var dbProduct = (await ProductService.GetProductAsync(ProductId)).Data;
            if (dbProduct is null)
            {
                message = $"Product with ID '{ProductId}' does not exist.";
                return;
            }

            product = dbProduct;
            product.IsEditing = true;
            btnText = "Update Product";
        }

        loading = false;
    }

    private void AddVariant() =>
        product.Variants.Add(new ProductVariant { IsNew = true, ProductId = ProductId });

    private void RemoveVariant(int productTypeId)
    {
        var variant = product.Variants.Find(v => v.ProductTypeId == productTypeId);
        if (variant is null)
        {
            return;
        }

        if (variant.IsNew)
        {
            product.Variants.Remove(variant);
        }
        else
        {
            variant.IsDeleted = true;
        }
    }

    private async Task AddOrUpdateProductAsync()
    {
        if (product.IsNew)
        {
            var results = await ProductService.CreateProductAsync(product);
            if (results is null)
            {
                return;
            }

            NavigationManager.NavigateTo($"admin/{nameof(Product)}/{results.Id}");
        }
        else
        {
            product.IsNew = false;
            product = await ProductService.UpdateProductAsync(product) ?? new Product();
            if (product.Id == 0)
            {
                return;
            }

            NavigationManager.NavigateTo($"admin/{nameof(Product)}/{product.Id}", true);
        }
    }

    private async Task DeleteProductAsync()
    {
        var confirmed = await JsRuntime.InvokeAsync<bool>("confirm",
            new object?[] { $"Do you really want to delete '{product.Title}'?" });
        if (!confirmed)
        {
            return;
        }

        await ProductService.DeleteProductAsync(product);
        NavigationManager.NavigateTo($"admin/{nameof(Product)}");
    }

    private async Task OnFilesChangeAsync(InputFileChangeEventArgs e)
    {
        const string format = "image/png";
        foreach (var image in e.GetMultipleFiles())
        {
            var resizedImage = await image.RequestImageFileAsync(format, 200, 200);
            var buffer = new byte[resizedImage.Size];
            // ReSharper disable once MustUseReturnValue
            await resizedImage.OpenReadStream().ReadAsync(buffer);
            var imageData = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
            product.Images.Add(new Image { Data = imageData });
        }
    }

    private void RemoveImage(int imageId)
    {
        var image = product.Images.FirstOrDefault(i => i.Id == imageId);
        if (image is null)
        {
            return;
        }

        product.Images.Remove(image);
    }
}
