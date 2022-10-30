namespace BlazorECommerce.Server.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductVariant>()
            .HasKey(variant => new { variant.ProductId, variant.ProductTypeId });

        modelBuilder.Entity<ProductType>().HasData(new List<ProductType>
        {
            new() { Id = 1, Name = "Default" },
            new() { Id = 2, Name = "Paperback" },
            new() { Id = 3, Name = "E-book" },
            new() { Id = 4, Name = "Audiobook" },
            new() { Id = 5, Name = "Stream" },
            new() { Id = 6, Name = "Blu-ray" },
            new() { Id = 7, Name = "VHS" },
            new() { Id = 8, Name = "PC" },
            new() { Id = 9, Name = "Playstation" },
            new() { Id = 10, Name = "Xbox" },
        });

        modelBuilder.Entity<Category>()
            .HasData(new List<Category>
            {
                new() { Id = 1, Name = "Books", Url = "books", },
                new() { Id = 2, Name = "Movies", Url = "movies", },
                new() { Id = 3, Name = "Video Games", Url = "video-games", },
            });

        modelBuilder.Entity<Product>()
            .HasData(new List<Product>
            {
                new()
                {
                    Id = 1,
                    Title = "Product 1 Title (Book)",
                    Description = "Product 1 Description",
                    ImageUrl =
                        "https://img.freepik.com/free-psd/cosmetic-product-packaging-mockup_1150-40284.jpg?w=2000",
                    CategoryId = 1,
                },
                new()
                {
                    Id = 2,
                    Title = "Product 2 Title (Movie)",
                    Description = "Product 2 Description",
                    ImageUrl =
                        "https://img.freepik.com/free-psd/cosmetic-product-packaging-mockup_1150-40282.jpg?w=2000",
                    CategoryId = 2,
                    Featured = true,
                },
                new()
                {
                    Id = 3,
                    Title = "Product 3 Title (Video Game)",
                    Description = "Product 3 Description",
                    ImageUrl =
                        "https://img.freepik.com/free-photo/pedestal-display-blank-podium-product_1048-16154.jpg?w=996",
                    CategoryId = 3,
                },
            });

        modelBuilder.Entity<ProductVariant>().HasData(new List<ProductVariant>
        {
            new() { ProductId = 1, ProductTypeId = 2, Price = 9.99M, OriginalPrice = 19.99M },
            new() { ProductId = 1, ProductTypeId = 3, Price = 19.99M, OriginalPrice = 29.99M },
            new() { ProductId = 2, ProductTypeId = 5, Price = 9.99M, OriginalPrice = 19.99M },
            new() { ProductId = 2, ProductTypeId = 6, Price = 19.99M, OriginalPrice = 29.99M },
            new() { ProductId = 3, ProductTypeId = 8, Price = 9.99M, OriginalPrice = 19.99M },
            new() { ProductId = 3, ProductTypeId = 10, Price = 19.99M, OriginalPrice = 29.99M },
        });
    }

    public virtual DbSet<Product> Products { get; set; } = default!;

    public virtual DbSet<Category> Categories { get; set; } = default!;

    public virtual DbSet<ProductType> ProductTypes { get; set; } = default!;

    public virtual DbSet<ProductVariant> ProductVariants { get; set; } = default!;
}
