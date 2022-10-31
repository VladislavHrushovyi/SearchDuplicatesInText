using SearchDuplicatesText.DataRepositories.InMemoryContext.Repositories.ProgressRepository;
using SearchDuplicatesText.DataRepositories.PostgreSqlContext.Repositories;

namespace SearchDuplicatesText.Services.PlagiarismCheckService;

public class BaseMethod
{
    protected readonly FileRepository _fileRepository;
    protected readonly IProgressRepository _progressRepository;

    public BaseMethod(FileRepository fileRepository, IProgressRepository progressRepository)
    {
        _fileRepository = fileRepository;
        _progressRepository = progressRepository;
    }
}