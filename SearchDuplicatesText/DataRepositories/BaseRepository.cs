namespace SearchDuplicatesText.DataRepositories;

public abstract class BaseRepository
{
    protected readonly AppDbContext DbContext;

    protected BaseRepository(AppDbContext dbContext) => DbContext = dbContext;
    
}