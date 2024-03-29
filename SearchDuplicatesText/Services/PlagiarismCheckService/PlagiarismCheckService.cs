using System.Text;
using SearchDuplicatesText.Models.Responses;
using SearchDuplicatesText.Services.FileService;

namespace SearchDuplicatesText.Services.PlagiarismCheckService;

public class PlagiarismCheckService
{
    private readonly FileHandler _fileHandler;

    public PlagiarismCheckService(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    public async Task<IResult> CheckPlagiarismByFile(IFormFile file, IPlagiarismMethod method, string progressName) 
    {
        var fileReader = await _fileHandler.GetFileReader(file);
        var dataFromFile = await _fileHandler.GetNewFileData(fileReader);

        return await StartMethod(dataFromFile, method, progressName);
    }

    public async Task<IResult> CheckPlagiarismByText(string text, IPlagiarismMethod method, string progressName)
    {
        var clearText = await _fileHandler.ClearText(new StringBuilder().Append(text));

        return await StartMethod(clearText, method, progressName);
    }

    private async Task<IResult> StartMethod(StringBuilder text, IPlagiarismMethod method, string progressName)
    {
        var preparedData = await method.GetPreparedData(text);
        var resultMethod = await method.StartMethod(preparedData, progressName);

        return Results.Ok(new MethodResultResponse(resultMethod));
    }
}