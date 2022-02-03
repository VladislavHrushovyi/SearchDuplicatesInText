using SearchDuplicatesText.DataRepositories;
using Microsoft.EntityFrameworkCore;

namespace SearchDuplicatesText.ServiceProviders;

public static class DbServiceProvider
{
    public static void AddDbService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => 
            opt.UseNpgsql(configuration.GetConnectionString("Database")));
    }
}