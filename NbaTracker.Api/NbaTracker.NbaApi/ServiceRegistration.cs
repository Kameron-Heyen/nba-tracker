using Microsoft.Extensions.DependencyInjection;

namespace NbaTracker.NbaApi;

public static class ServiceRegistration
{
    public static IServiceCollection AddUtilityServices(this IServiceCollection services)
    {
        services.AddScoped<INbaApi, NbaApi>();
        
        return services;
    }
}