﻿namespace BlazorECommerce.Shared.DTOs;

public class ProductSearchResult
{
    public List<Product> Products { get; set; } = new();

    public int Pages { get; set; }

    public int CurrentPage { get; set; }
}
