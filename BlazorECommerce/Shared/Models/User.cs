namespace BlazorECommerce.Shared.Models;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

    public DateTime DateCreated { get; set; } = DateTime.Now;

    public Address Address { get; set; } = default!;

    public string Role { get; set; } = "Customer";
}
