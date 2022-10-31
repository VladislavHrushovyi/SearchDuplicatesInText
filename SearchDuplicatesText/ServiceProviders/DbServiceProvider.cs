using Microsoft.EntityFrameworkCore;
using SearchDuplicatesText.DataRepositories.InMemoryContext;
using SearchDuplicatesText.DataRepositories.PostgreSqlContext;

namespace SearchDuplicatesText.ServiceProviders;

public static class DbServiceProvider
{
    public static void AddDbService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => 
            opt.UseNpgsql(configuration.GetConnectionString("Database")));

        services.AddDbContext<InMemoryContext>(opt =>
        {
            opt.UseInMemoryDatabase(databaseName: "SystemDb");
        });
    }
}