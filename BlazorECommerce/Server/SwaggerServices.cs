namespace BlazorECommerce.Server;

public static class SwaggerServices
{
    public static IServiceCollection ConfigureSwaggerServices(this IServiceCollection services)
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JSON Web Token based security",
        };

        var securityReq = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                Array.Empty<string>()
            }
        };

        var info = new OpenApiInfo
        {
            Version = "v1",
            Title = "Minimal API - JWT Authentication with Swagger demo",
            Description = "Implementing JWT Authentication in Minimal API",
        };

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", info);
                options.AddSecurityDefinition("Bearer", securityScheme);
                options.AddSecurityRequirement(securityReq);
            });

        return services;
    }
}
