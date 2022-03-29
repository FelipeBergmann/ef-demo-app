using Microsoft.OpenApi.Models;

public static class UseSwaggerExtension
{
    public static IServiceCollection UseSwaggerGen(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "EF Demo App", Description = "Teste com Minimal APIs", Version = "v1" });
        });

        return services;
    }
}