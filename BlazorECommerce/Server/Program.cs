var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var appSettings = config.Get<AppSettings>() ?? new AppSettings();

var services = builder.Services;

services.AddControllersWithViews();
services.AddRazorPages();

services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

_ = appSettings.DbProvider switch
{
    null => throw new Exception("No provider found"),

    DbProvider.Sqlite => services.AddDbContext<DatabaseContext, SqliteDatabaseContext>(options =>
        options.UseSqlite(config.GetConnectionString(nameof(DbProvider.Sqlite)) ?? string.Empty)),

    DbProvider.SqlServer => services.AddDbContext<DatabaseContext, SqlServerDatabaseContext>(options =>
        options.UseSqlServer(config.GetConnectionString(nameof(DbProvider.SqlServer)) ?? string.Empty)),

    DbProvider.InMemory => services.AddDbContext<DatabaseContext, InMemoryDatabaseContext>(options =>
        options.UseInMemoryDatabase(nameof(InMemoryDatabaseContext))),

    _ => throw new Exception($"Unsupported provider: {appSettings.DbProvider}")
};

services
    .AddScoped(_ => appSettings)
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<ICartService, CartService>()
    .AddScoped<IOrderService, OrderService>()
    .AddScoped<IPaymentService, PaymentService>()
    .AddScoped<IAddressService, AddressService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IProductTypeService, ProductTypeService>()
    .AddScoped<ICategoryService, CategoryService>();

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.AuthSettings.AuthToken)),
        ValidateIssuer = false,
        ValidateAudience = false
    });

services.Configure<JsonOptions>(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

if (appSettings.DbProvider is not null)
{
    var databaseContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
    switch (appSettings.DbProvider)
    {
        case DbProvider.InMemory:
            databaseContext.Database.EnsureCreated();
            break;
        case DbProvider.SqlServer:
        case DbProvider.Sqlite:
            databaseContext.Database.Migrate();
            break;
        default:
            throw new Exception($"Unsupported provider: {appSettings.DbProvider}");
    }
}

var apiGroup = app.MapGroup("api");

apiGroup
    .MapAuthApi()
    .MapCartApi()
    .MapOrderApi()
    .MapPaymentApi()
    .MapAddressApi()
    .MapProductApi()
    .MapCategoryApi()
    .MapProductTypeApi();

app.Run();
