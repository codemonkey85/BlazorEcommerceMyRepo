namespace BlazorECommerce.Server.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(new List<Product>()
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
        });
    }

    public virtual DbSet<Product> Products { get; set; } = default!;
}
