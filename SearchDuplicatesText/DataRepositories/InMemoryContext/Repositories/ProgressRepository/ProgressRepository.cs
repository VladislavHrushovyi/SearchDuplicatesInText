using System.Data.Entity;
using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.DataRepositories.InMemoryContext.Repositories.ProgressRepository;

public class ProgressRepository : IProgressRepository
{
    private readonly InMemoryContext? _inMemoryContext;

    public ProgressRepository(InMemoryContext? inMemoryContext)
    {
        _inMemoryContext = inMemoryContext;
    }

    public async Task<ProgressModel> AddProgress(ProgressModel model)
    {
        var progress =  await _inMemoryContext.Progress.AddAsync(model);
        await _inMemoryContext.SaveChangesAsync();

        return progress.Entity;
    }

    public async Task<ProgressModel> GetProgressByName(string name)
    {
        var progress = _inMemoryContext.Progress.Where(p => p.NameOfProgress == name)
            .FirstOrDefault();

        return progress;
    }

    public async Task UpdateProgress(ProgressModel model)
    {
        var updateProgress = _inMemoryContext.Progress.Update(model);
        await _inMemoryContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteProgress(ProgressModel model)
    {
        _inMemoryContext.Progress.Remove(model);
        await _inMemoryContext.SaveChangesAsync();
    }
}