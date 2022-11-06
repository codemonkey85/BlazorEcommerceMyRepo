namespace BlazorECommerce.Shared.Models;

public class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public List<Image> Images { get; set; } = new();

    public Category? Category { get; set; }

    public int? CategoryId { get; set; }

    public bool Featured { get; set; }

    public List<ProductVariant> Variants { get; set; } = new();

    public bool IsVisible { get; set; } = true;

    public bool IsDeleted { get; set; } = false;

    [NotMapped] public bool IsEditing { get; set; } = false;

    [NotMapped] public bool IsNew { get; set; } = false;
}
