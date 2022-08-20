using System.Diagnostics;
using SearchDuplicatesText.Services.MakeDataForMethodsService;

namespace SearchDuplicatesText.Services.FileService;

public class UploadFileService
{
    private readonly FileHandler _fileHandler;
    private readonly ConvertTextToDataMethod _convertText;

    public UploadFileService(ConvertTextToDataMethod convertText, FileHandler fileHandler)
    {
        _convertText = convertText;
        _fileHandler = fileHandler;
    }

    public async Task<IResult> SaveUploadFile(IFormFile file)
    {
        var watch = new Stopwatch();
        watch.Start();
        var fileReader = await _fileHandler.GetFileReader(file);
        var filterTextResult = await _fileHandler.GetNewFileData(fileReader);
        var fileName = await _fileHandler.SaveFile(fileReader, filterTextResult);

        await _convertText.CreateShingleFile(filterTextResult, fileName);
        await _convertText.CreateNgramFile(filterTextResult, fileName);
        
        watch.Stop();
        Console.WriteLine($"Add {fileName} on {watch.Elapsed}");
        return Results.Ok(new
        {
            name = fileName
        });
    }
    
}