using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace SearchDuplicatesText.Services.FileService.FileReaders;

public class PdfFileReader : IFileReader
{
    public IFormFile File { get; }
    
    public PdfFileReader(IFormFile file) => File = file;
    public async Task<StringBuilder> GetDataAsync()
    {
        var bytesFile = await File.GetBytes();
        var textBuilder = new StringBuilder();
        using var pdf = PdfDocument.Open(bytesFile);
        foreach (var page in pdf.GetPages())
        {
            textBuilder.Append(ContentOrderTextExtractor.GetText(page));
        }

        return textBuilder;
    }
}