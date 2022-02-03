using SearchDuplicatesText.Services.FileService;
using SearchDuplicatesText.Services.MakeDataForMethodsService;

namespace SearchDuplicatesText.Services.PlagiarismCheckService;

public class PlagiarismCheckService
{
    private readonly FileHandler _fileHandler;
    private readonly ConvertTextToDataMethod _convertText;

    public PlagiarismCheckService(FileHandler fileHandler, ConvertTextToDataMethod convertText)
    {
        _fileHandler = fileHandler;
        _convertText = convertText;
    }

    public async Task<IResult> CheckPlagiarismByShingle(IFormFile file, IPlagiarismMethod method)
    {
        var fileReader = await _fileHandler.GetFileReader(file);
        var dataFromFile = await _fileHandler.GetNewFileData(fileReader);
        var shingles = await _convertText.GetShinglesHash(dataFromFile);
        //var fileName = await _fileHandler.SaveFile(fileReader, dataFromFile);
        var resultMethod = await method.StartMethod(shingles);
        //await _convertText.CreateShingleFile(dataFromFile, fileName);
        //await _convertText.CreateNgramFile(dataFromFile, fileName);

        return Results.Json(
            new
            {
                resultMethod, resultMethod.Count
            });
    }
}