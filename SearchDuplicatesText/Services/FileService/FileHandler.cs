using System.Text;
using SearchDuplicatesText.Services.FileService.FileReaders;

namespace SearchDuplicatesText.Services.FileService;

public class FileHandler
{
    
    private readonly TextClearing _textClearing;
    private readonly WordNormalizer _wordNormalizer;

    public FileHandler(WordNormalizer wordNormalizer, TextClearing textClearing)
    {
        _wordNormalizer = wordNormalizer;
        _textClearing = textClearing;
    }

    public Task<IFileReader> GetFileReader(IFormFile file)
    {
        var typeFile = Path.GetExtension(file.FileName);
        return Task.FromResult<IFileReader>(typeFile switch
        {
            ".txt" => new TxtFileReader(file),
            ".pdf" => new PdfFileReader(file),
            ".docx" => new WordFileReader(file),
            _ => null!
        });
    }
    
    public async Task<StringBuilder> GetNewFileData(IFileReader fileReader)
    {
        var filteredText = new StringBuilder();
        var handleFileResult = await fileReader.GetDataAsync();
        var textFromUpload = await _textClearing.ClearTextAsync(handleFileResult);

        foreach (var s in textFromUpload!.Split(" "))
        {
            if (!await _wordNormalizer.IsWrongWord(s))
            {
                filteredText.Append(s + " ");
            }
        }

        return filteredText;
    }

    public async Task<string> SaveFile(IFileReader fileReader, StringBuilder filterTextResult)
    {
        var fileName = await fileReader.CreateFileName();
        await File.WriteAllTextAsync($"./Files/{fileName}", filterTextResult.ToString());
        return fileName;
    }
}