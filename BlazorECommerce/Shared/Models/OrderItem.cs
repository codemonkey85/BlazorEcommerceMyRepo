﻿namespace BlazorECommerce.Shared.Models;

public class OrderItem
{
    public Order Order { get; set; } = default!;

    public int OrderId { get; set; }

    public Product Product { get; set; } = default!;

    public int ProductId { get; set; }

    public ProductType ProductType { get; set; } = default!;

    public int ProductTypeId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }
}
