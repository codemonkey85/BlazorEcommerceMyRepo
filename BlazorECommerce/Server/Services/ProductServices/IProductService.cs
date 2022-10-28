﻿namespace BlazorECommerce.Server.Services.ProductServices;

public interface IProductService
{
    Task<ServiceResponse<List<Product>>> GetProductsAsync();

    Task<ServiceResponse<Product>> GetProductAsync(int productId);
}