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
    DbProvider.Sqlite => services.AddDbContext<DatabaseContext, SqliteDatabaseContext>(options => options.UseSqlite(config.GetConnectionString(nameof(DbProvider.Sqlite)) ?? string.Empty)),
    DbProvider.SqlServer => services.AddDbContext<DatabaseContext, SqlServerDatabaseContext>(options => options.UseSqlServer(config.GetConnectionString(nameof(DbProvider.SqlServer)) ?? string.Empty)),
    DbProvider.InMemory => services.AddDbContext<DatabaseContext, InMemoryDatabaseContext>(options => options.UseInMemoryDatabase(nameof(InMemoryDatabaseContext))),
    _ => throw new Exception($"Unsupported provider: {appSettings.DbProvider}")
};

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

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

var apiGroup = app.MapGroup("api");

apiGroup.MapProductApi();

app.Run();
