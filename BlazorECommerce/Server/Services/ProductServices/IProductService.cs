﻿namespace BlazorECommerce.Server.Services.ProductServices;

public interface IProductService
{
    Task<ServiceResponse<List<Product>>> GetProductsAsync();

    Task<ServiceResponse<Product>> GetProductAsync(int productId);

    Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);

    Task<ServiceResponse<ProductSearchResult>> SearchProductsAsync(string searchText, int page);

    Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);

    Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync();
}
