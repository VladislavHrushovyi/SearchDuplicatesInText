using System.Text;

namespace SearchDuplicatesText.Services.FileService.FileReaders;

public interface IFileReader
{
    IFormFile File { get; }
    public Task<StringBuilder> GetDataAsync();

    public async Task<string> CreateFileName()
    {
        var guid = Guid.NewGuid();
        return await Task.Run(() => $"{guid}-{File.FileName.Split(".")[0]}.txt");
    }
}