var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;

services
    .AddBlazoredLocalStorage()
    .AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddScoped<ICartService, CartService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<ICategoryService, CategoryService>();

await builder.Build().RunAsync();
