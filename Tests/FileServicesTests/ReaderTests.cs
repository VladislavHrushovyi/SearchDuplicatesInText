using SearchDuplicatesText.Services.FileService.FileReaders;
using Tests.Extension;

namespace Tests.FileServicesTests;

public class ReaderTests
{
    private  IFileReader? _customReader;
    private const string PathToRes = "../../../Resources";


    [Fact]
    public async Task CheckDataFromTxtReader()
    {
        await using var file = File.OpenRead($"{PathToRes}/ІНДЗ.txt");
        _customReader = new TxtFileReader(file.ConvertToFormFile());

        var actual = await _customReader.GetDataAsync();
        
        Assert.NotNull(actual);
        Assert.NotEqual(0, actual.Length);
    }
    
    [Fact]
    public async Task CheckDataFromPdfReader()
    {
        await using var file = File.OpenRead($"{PathToRes}/ІНДЗ_ГрушовийВО_ПКМ.pdf");
        _customReader = new PdfFileReader(file.ConvertToFormFile());

        var actual = await _customReader.GetDataAsync();
        
        Assert.NotNull(actual);
        Assert.NotEqual(0, actual.Length);
    }
    
    [Fact]
    public async Task CheckDataFromDocxReader()
    {
        await using var file = File.OpenRead($"{PathToRes}/ІНДЗ_ГрушовийВО_ПКМ.docx");
        _customReader = new WordFileReader(file.ConvertToFormFile());

        var actual = await _customReader.GetDataAsync();
        
        Assert.NotNull(actual);
        Assert.NotEqual(0, actual.Length);
    }

    [Fact]
    public async Task CheckValidFileNameWithFormat__docx()
    {
        await using var file = File.OpenRead($"{PathToRes}/ІНДЗ_ГрушовийВО_ПКМ.docx");
        _customReader = new WordFileReader(file.ConvertToFormFile());

        var result = await _customReader.CreateFileName();
        
        Assert.Matches($"\\w+\\.txt", result);
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Length);
    }
    
    [Fact]
    public async Task CheckValidFileNameWithFormat__pdf()
    {
        await using var file = File.OpenRead($"{PathToRes}/ІНДЗ_ГрушовийВО_ПКМ.pdf");
        _customReader = new PdfFileReader(file.ConvertToFormFile());

        var result = await _customReader.CreateFileName();
        
        Assert.Matches($"\\w+\\.txt", result);
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Length);
    }
    
    [Fact]
    public async Task CheckValidFileNameWithFormat__txt()
    {
        await using var file = File.OpenRead($"{PathToRes}/ІНДЗ.txt");
        _customReader = new WordFileReader(file.ConvertToFormFile());

        var result = await _customReader.CreateFileName();
        
        Assert.Matches($"\\w+\\.txt", result);
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Length);
    }
}