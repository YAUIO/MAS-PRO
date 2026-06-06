using B2.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace B2.Data;

public static class DataConfigurationExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddDbContext<B2DbContext>(options =>
        {
            options.UseSqlite("../../b2.db");
        });
        return services;
    }
}