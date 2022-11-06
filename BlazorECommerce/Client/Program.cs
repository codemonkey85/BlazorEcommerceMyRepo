var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddBlazoredLocalStorage()
    .AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<ICartService, CartService>()
    .AddScoped<IOrderService, OrderService>()
    .AddScoped<IAddressService, AddressService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<ICategoryService, CategoryService>()
    .AddScoped<IProductTypeService, ProductTypeService>()
    .AddOptions()
    .AddAuthorizationCore()
    .AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
