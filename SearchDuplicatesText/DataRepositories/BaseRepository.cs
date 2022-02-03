namespace SearchDuplicatesText.DataRepositories;

public abstract class BaseRepository
{
    protected AppDbContext dbContext;

    protected BaseRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}