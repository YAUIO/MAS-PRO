using Microsoft.Extensions.DependencyInjection;

namespace B2.App;

public static class AppConfigurationExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services;
    }
}