using System.Text;

namespace SearchDuplicatesText.Services.FileService.FileReaders;

public class TxtFileReader : IFileReader
{
    public IFormFile File { get; }
    public TxtFileReader(IFormFile file) => File = file;
    public async Task<StringBuilder> GetDataAsync()
    {
        var fileStream = File.OpenReadStream();
        var textBuilder = new StringBuilder();
        using var fileReader = new StreamReader(fileStream);
        textBuilder.Append(await fileReader.ReadToEndAsync());

        return textBuilder;
    }
}