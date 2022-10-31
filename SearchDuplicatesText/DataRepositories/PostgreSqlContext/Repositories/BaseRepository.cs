namespace SearchDuplicatesText.DataRepositories.PostgreSqlContext.Repositories;

public abstract class BaseRepository
{
    protected readonly AppDbContext DbContext;

    protected BaseRepository(AppDbContext dbContext) => DbContext = dbContext;
    
}