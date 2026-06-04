using Microsoft.Extensions.DependencyInjection;

namespace B2.Data;

public static class DataConfigurationExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services;
    }
}