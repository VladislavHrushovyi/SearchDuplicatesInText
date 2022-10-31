using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.DataRepositories.InMemoryContext.Repositories.ProgressRepository;

public interface IProgressRepository
{
    public Task<ProgressModel> AddProgress(ProgressModel model);
    public Task<ProgressModel> GetProgressByName(string name);
    public Task UpdateProgress(ProgressModel model);
    public Task DeleteProgress(ProgressModel model);
}