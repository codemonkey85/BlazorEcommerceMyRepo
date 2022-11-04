namespace BlazorECommerce.Shared.Models;

public class ProductType
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [NotMapped] public bool IsEditing { get; set; } = false;

    [NotMapped] public bool IsNew { get; set; } = false;
}
