using Microsoft.EntityFrameworkCore;
using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.DataRepositories.InMemoryContext;

public class InMemoryContext : DbContext
{
    public InMemoryContext(DbContextOptions opt) : base(opt)
    {
        
    }

    public DbSet<ProgressModel>? Progress { get; set; }
}