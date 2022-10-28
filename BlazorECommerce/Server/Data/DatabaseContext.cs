namespace BlazorECommerce.Server.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(new List<Category>
        {
            new() { Id = 1, Name = "Books", Url = "books", },
            new() { Id = 2, Name = "Movies", Url = "movies", },
            new() { Id = 3, Name = "Video Games", Url = "video-games", },
        });

        modelBuilder.Entity<Product>().HasData(new List<Product>
        {
            new()
            {
                Id = 1,
                Title = "Product 1 Title (Book)",
                Description = "Product 1 Description",
                ImageUrl =
                    "https://img.freepik.com/free-psd/cosmetic-product-packaging-mockup_1150-40284.jpg?w=2000",
                Price = 9.99M,
                CategoryId = 1,
            },
            new()
            {
                Id = 2,
                Title = "Product 2 Title (Movie)",
                Description = "Product 2 Description",
                ImageUrl =
                    "https://img.freepik.com/free-psd/cosmetic-product-packaging-mockup_1150-40282.jpg?w=2000",
                Price = 9.99M,
                CategoryId = 2,
            },
            new()
            {
                Id = 3,
                Title = "Product 3 Title (Video Game)",
                Description = "Product 3 Description",
                ImageUrl =
                    "https://img.freepik.com/free-photo/pedestal-display-blank-podium-product_1048-16154.jpg?w=996",
                Price = 9.99M,
                CategoryId = 3,
            },
        });
    }

    public virtual DbSet<Product> Products { get; set; } = default!;

    public virtual DbSet<Category> Categories { get; set; } = default!;
}
