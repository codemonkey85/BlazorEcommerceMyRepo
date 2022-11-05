namespace BlazorECommerce.Shared.Models;

public class ProductVariant
{
    [JsonIgnore] public Product? Product { get; set; } = default!;

    public int ProductId { get; set; }

    public ProductType? ProductType { get; set; } = default!;

    public int ProductTypeId { get; set; }

    [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")] public decimal OriginalPrice { get; set; }

    public bool IsVisible { get; set; } = true;

    public bool IsDeleted { get; set; } = false;

    [NotMapped] public bool IsEditing { get; set; } = false;

    [NotMapped] public bool IsNew { get; set; } = false;
}
