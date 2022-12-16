namespace BlazorECommerce.Server.Services.ProductServices;

public record ProductService(DatabaseContext DatabaseContext) : IProductService
{
    public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await DatabaseContext.Products
                .Where(product => product.IsVisible && !product.IsDeleted)
                .Include(product => product.Variants.Where(variant => variant.IsVisible && !variant.IsDeleted))
                .Include(product => product.Images)
                .ToListAsync()
        };
        return response;
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(IAuthService authService, int productId)
    {
        var query = authService.IsUserInRole(Constants.Admin) switch
        {
            true => DatabaseContext.Products
                .Include(product => product.Variants.Where(variant => !variant.IsDeleted))
                .ThenInclude(variant => variant.ProductType)
                .Include(product => product.Images)
                .FirstOrDefaultAsync(product => product.Id == productId && !product.IsDeleted),

            false => DatabaseContext.Products
                .Include(product => product.Variants.Where(variant => variant.IsVisible && !variant.IsDeleted))
                .ThenInclude(variant => variant.ProductType)
                .Include(product => product.Images)
                .FirstOrDefaultAsync(product => product.Id == productId && product.IsVisible && !product.IsDeleted),
        };

        return await query switch
        {
            null => new ServiceResponse<Product> { Success = false, Message = $"{nameof(Product)} not found." },
            var product => new ServiceResponse<Product>(product)
        };
    }

    public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl) => new
    (
        await DatabaseContext.Products
            .Where(product => product.Category != null && product.Category.Url == categoryUrl && product.IsVisible &&
                              !product.IsDeleted)
            .Include(product => product.Variants.Where(variant => variant.IsVisible && !variant.IsDeleted))
            .Include(product => product.Images)
            .ToListAsync()
    );

    private IQueryable<Product> FindProductsBySearchStringAsync(string searchText) =>
        DatabaseContext.Products
            .Include(product => product.Variants)
            .Include(product => product.Images)
            .Where(product => product.IsVisible && !product.IsDeleted &&
                              (product.Title.ToLower().Contains(searchText.ToLower()) ||
                               product.Description.ToLower().Contains(searchText.ToLower())));

    public async Task<ServiceResponse<ProductSearchResult>> SearchProductsAsync(string searchText, int page)
    {
        const int pageSize = 2;
        var productsFound = await FindProductsBySearchStringAsync(searchText).ToListAsync();
        var pageCount = (int)Math.Ceiling(productsFound.Count / (float)pageSize);

        var products = productsFound
            .OrderBy(product => product.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new ServiceResponse<ProductSearchResult>
        (
            new ProductSearchResult { Products = products, CurrentPage = page, Pages = pageCount }
        );
    }

    public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText) => new
    (
        await FindProductsBySearchStringAsync(searchText)
            .Select(product => product.Title)
            .ToListAsync()
    );

    public async Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync() => new
    (
        await DatabaseContext.Products
            .Where(product => product.Featured && product.IsVisible && !product.IsDeleted)
            .Include(product => product.Variants.Where(variant => variant.IsVisible && !variant.IsDeleted))
            .Include(product => product.Images)
            .ToListAsync()
    );

    public async Task<ServiceResponse<List<Product>>> GetAdminProductsAsync()
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await DatabaseContext.Products
                .Where(product => !product.IsDeleted)
                .Include(product => product.Variants.Where(variant => !variant.IsDeleted))
                .ThenInclude(variant => variant.ProductType)
                .Include(product => product.Images)
                .ToListAsync()
        };
        return response;
    }

    public async Task<ServiceResponse<Product>> CreateProductAsync(Product product)
    {
        // TODO: is this step needed?
        //foreach (var variant in product.Variants)
        //{
        //    variant.ProductType = null;
        //}

        DatabaseContext.Products.Add(product);
        await DatabaseContext.SaveChangesAsync();
        return new ServiceResponse<Product>(product);
    }

    public async Task<ServiceResponse<Product>> UpdateProductAsync(Product product)
    {
        var dbProduct = await DatabaseContext.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == product.Id);
        if (dbProduct is null)
        {
            return new ServiceResponse<Product> { Success = false, Message = $"{nameof(Product)} not found." };
        }

        dbProduct.Title = product.Title;
        dbProduct.Description = product.Description;
        dbProduct.ImageUrl = product.ImageUrl;
        dbProduct.CategoryId = product.CategoryId;
        dbProduct.IsVisible = product.IsVisible;
        dbProduct.Featured = product.Featured;

        var productImages = dbProduct.Images;
        DatabaseContext.Images.RemoveRange(productImages);

        dbProduct.Images = product.Images;

        foreach (var variant in product.Variants)
        {
            var dbVariant = await DatabaseContext.ProductVariants.SingleOrDefaultAsync(v =>
                v.ProductId == variant.ProductId && v.ProductTypeId == variant.ProductTypeId);

            if (dbVariant is null)
            {
                // TODO: is this step needed?
                //variant.ProductType = null;
                DatabaseContext.ProductVariants.Add(variant);
            }
            else
            {
                dbVariant.ProductTypeId = variant.ProductTypeId;
                dbVariant.Price = variant.Price;
                dbVariant.OriginalPrice = variant.OriginalPrice;
                dbVariant.IsVisible = variant.IsVisible;
                dbVariant.IsDeleted = variant.IsDeleted;
            }
        }

        await DatabaseContext.SaveChangesAsync();
        return new ServiceResponse<Product>(product);
    }

    public async Task<ServiceResponse<bool>> DeleteProductAsync(int productId)
    {
        var dbProduct = await DatabaseContext.Products.FindAsync(productId);
        if (dbProduct is null)
        {
            return new ServiceResponse<bool>
            {
                Success = false, Data = false, Message = $"{nameof(Product)} not found."
            };
        }

        dbProduct.IsDeleted = true;
        await DatabaseContext.SaveChangesAsync();
        return new ServiceResponse<bool>(true);
    }
}
