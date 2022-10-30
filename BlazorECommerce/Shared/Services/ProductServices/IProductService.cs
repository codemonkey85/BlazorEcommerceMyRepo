﻿namespace BlazorECommerce.Shared.Services.ProductServices;

public interface IProductService
{
    event Action? ProductsChanged;

    List<Product> Products { get; set; }

    string Message { get; set; }

    int CurrentPage { get; set; }

    int PageCount { get; set; }

    string LastSearchText { get; set; }

    Task GetProductsAsync(string? categoryUrl = null);

    Task<ServiceResponse<Product>> GetProductAsync(int productId);

    Task SearchProductsAsync(string searchText, int page);

    Task<List<string>> GetProductSearchSuggestionsAsync(string searchText);
}
