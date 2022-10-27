namespace BlazorECommerce.Rcl.Services;

public interface IProductService
{
    List<Product> Products { get; set; }

    Task GetProductsAsync();
}
