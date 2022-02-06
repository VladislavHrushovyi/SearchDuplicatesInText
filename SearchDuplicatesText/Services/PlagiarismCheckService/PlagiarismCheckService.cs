using SearchDuplicatesText.Services.FileService;

namespace SearchDuplicatesText.Services.PlagiarismCheckService;

public class PlagiarismCheckService
{
    private readonly FileHandler _fileHandler;

    public PlagiarismCheckService(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    public async Task<IResult> CheckPlagiarism<T>(IFormFile file, T method) where T : IPlagiarismMethod
    {
        var fileReader = await _fileHandler.GetFileReader(file);
        var dataFromFile = await _fileHandler.GetNewFileData(fileReader);
        var shingles = await method.GetPreparedData(dataFromFile);
        var resultMethod = await method.StartMethod(shingles);

        return Results.Json(
            new
            {
                resultMethod, resultMethod.Count
            });
    }
}