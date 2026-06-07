using B2.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace B2.App;

public static class AppConfigurationExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        
        return services;
    }
}