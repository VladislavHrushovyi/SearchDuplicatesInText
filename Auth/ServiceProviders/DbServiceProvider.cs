using Auth.UserDbContext;
using Microsoft.EntityFrameworkCore;

namespace Auth.ServiceProviders;

public static class DbServiceProvider
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserContext>(o => 
            o.UseNpgsql(config.GetConnectionString("Database"))
            
            );
        return services;
    }
}