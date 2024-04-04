using Infrastructure.Services;

namespace WebApp.Configurations;

public static class ServiceConfiguration
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AddressManager>();
        services.AddScoped<CategoryService>();
        services.AddScoped<CourseService>();
    }
}
