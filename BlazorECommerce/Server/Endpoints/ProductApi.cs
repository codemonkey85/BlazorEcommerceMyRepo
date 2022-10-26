namespace BlazorECommerce.Server.Endpoints;

public static class ProductApi
{
    private static readonly List<Product> Products = new()
    {
        new Product
        {
            Id = 1,
            Title = "Product 1 Title",
            Description = "Product 1 Description",
            ImageUrl =
                "https://img.freepik.com/free-psd/cosmetic-product-packaging-mockup_1150-40284.jpg?w=2000",
            Price = 9.99M
        },
        new Product
        {
            Id = 2,
            Title = "Product 2 Title",
            Description = "Product 2 Description",
            ImageUrl =
                "https://img.freepik.com/free-psd/cosmetic-product-packaging-mockup_1150-40282.jpg?w=2000",
            Price = 9.99M
        },
        new Product
        {
            Id = 3,
            Title = "Product 3 Title",
            Description = "Product 3 Description",
            ImageUrl =
                "https://img.freepik.com/free-photo/pedestal-display-blank-podium-product_1048-16154.jpg?w=996",
            Price = 9.99M
        },
    };

    public static IEndpointRouteBuilder MapProductApi(this IEndpointRouteBuilder apiGroup)
    {
        var productGroup = apiGroup.MapGroup(nameof(Product));

        productGroup.MapGet("/", GetProducts);

        /*
            productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/{id:guid}", GetEntity);
            productGroup.MapPost("/", PostEntity);
            productGroup.MapPut("/{id:guid}", PutEntity);
            productGroup.MapDelete("/{id:guid}", DeleteEntity);
         */

        return apiGroup;
    }

    private static Task<Ok<List<Product>>> GetProducts() => Task.FromResult(TypedResults.Ok(Products));
}
