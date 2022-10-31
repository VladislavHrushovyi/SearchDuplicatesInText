using Microsoft.EntityFrameworkCore;
using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.DataRepositories.PostgreSqlContext;

public class AppDbContext : DbContext
{
    public DbSet<ShingleFile> SingleFiles { get; set; }
    public DbSet<NgramFile> NgramFiles { get; set; }

    public DbSet<ExpFile> ExpFiles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
        
    }
}