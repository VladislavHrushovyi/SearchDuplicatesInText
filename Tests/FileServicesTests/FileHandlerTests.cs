using System.Text;
using Moq;
using SearchDuplicatesText.Services.FileService;
using SearchDuplicatesText.Services.FileService.FileReaders;
using Tests.Extension;

namespace Tests.FileServicesTests;

public class FileHandlerTests
{
    private readonly Mock<FileHandler> _fileHandler;

    public FileHandlerTests()
    {
        _fileHandler = new Mock<FileHandler>(new WordNormalizer(), new TextClearing());
    }
    

    [Fact]
    public async Task CheckGetCustomFileReaderByFileExtension__pdf()
    {
        var file = File.OpenRead("../../../Resources/ІНДЗ_ГрушовийВО_ПКМ.pdf");
        var result =  await _fileHandler.Object.GetFileReader(file.FileStreamToFormFile());

        Assert.IsType<PdfFileReader>(result);
    }
    
    [Fact]
    public async Task CheckGetCustomFileReaderByFileExtension__docx()
    {
        var file = File.OpenRead("../../../Resources/ІНДЗ_ГрушовийВО_ПКМ.docx");
        var result =  await _fileHandler.Object.GetFileReader(file.FileStreamToFormFile());
        
        Assert.IsType<WordFileReader>(result);
    }
    
    [Fact]
    public async Task CheckGetCustomFileReaderByFileExtension__text()
    {
        var file = File.OpenRead("../../../Resources/ІНДЗ.txt");
        var result =  await _fileHandler.Object.GetFileReader(file.FileStreamToFormFile());
        
        Assert.IsType<TxtFileReader>(result);
    }

    [Fact]
    public async Task CheckDataFromFileReader__pdf()
    {
        await using var stream = File.OpenRead("../../../Resources/ІНДЗ_ГрушовийВО_ПКМ.pdf");
        var reader =  await _fileHandler.Object.GetFileReader(stream.ConvertToFormFile());
        
        var result = await _fileHandler.Object.GetNewFileData(reader);
        
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Length);
        Assert.Equal(result.ToString().ToLower(), result.ToString());
    }
    
    [Fact]
    public async Task CheckDataFromFileReader__docx()
    {
        await using var stream = File.OpenRead("../../../Resources/ІНДЗ_ГрушовийВО_ПКМ.docx");
        var reader =  await _fileHandler.Object.GetFileReader(stream.ConvertToFormFile());
        
        var result = await _fileHandler.Object.GetNewFileData(reader);
        
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Length);
        Assert.Equal(result.ToString().ToLower(), result.ToString());
    }
    
    [Fact]
    public async Task CheckDataFromFileReader__txt()
    {
        await using var stream = File.OpenRead("../../../Resources/ІНДЗ.txt");
        var reader =  await _fileHandler.Object.GetFileReader(stream.ConvertToFormFile());
        
        var result = await _fileHandler.Object.GetNewFileData(reader);
        
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Length);
        Assert.Equal(result.ToString().ToLower(), result.ToString());
    }

    [Fact]
    public async Task CheckCreateTextFileWithNameWithReader__pdf()
    {
        await using var stream = File.OpenRead("../../../Resources/ІНДЗ_ГрушовийВО_ПКМ.pdf");
        var reader = await _fileHandler.Object.GetFileReader(stream.ConvertToFormFile());
        var filteredTextFromReader = await _fileHandler.Object.GetNewFileData(reader);
        
        var createdFileName = await _fileHandler.Object.CreateFile(reader, filteredTextFromReader);

        var result = Path.Exists($"./Files/{createdFileName}");
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task CheckCreateTextFileWithNameWithReader__docx()
    {
        await using var stream = File.OpenRead("../../../Resources/ІНДЗ_ГрушовийВО_ПКМ.docx");
        var reader = await _fileHandler.Object.GetFileReader(stream.ConvertToFormFile());
        var filteredTextFromReader = await _fileHandler.Object.GetNewFileData(reader);
        
        var createdFileName = await _fileHandler.Object.CreateFile(reader, filteredTextFromReader);

        var result = Path.Exists($"./Files/{createdFileName}");
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task CheckCreateTextFileWithNameWithReader__txt()
    {
        await using var stream = File.OpenRead("../../../Resources/ІНДЗ.txt");
        var reader = await _fileHandler.Object.GetFileReader(stream.ConvertToFormFile());
        var filteredTextFromReader = await _fileHandler.Object.GetNewFileData(reader);
        
        var createdFileName = await _fileHandler.Object.CreateFile(reader, filteredTextFromReader);

        var result = Path.Exists($"./Files/{createdFileName}");
        
        Assert.True(result);
    }

    [Fact]
    public async Task CheckClearTextByStringData()
    {
        var text = await File.ReadAllTextAsync("../../../Resources/ІНДЗ.txt");
        var data = new StringBuilder(text);

        var actual = await _fileHandler.Object.ClearText(data);
        
        Assert.NotNull(actual);
        Assert.NotEqual(0, actual.Length);
        Assert.Equal(actual.ToString().ToLower(), actual.ToString());
    }

    [Fact]
    public async Task CheckCreatingFileByTextData()
    {
        var text = await File.ReadAllTextAsync("../../../Resources/ІНДЗ.txt");
        var createdFileName = await _fileHandler.Object.CreateFile(text);
        
        var actual = Path.Exists($"./Files/{createdFileName}");
        
        Assert.True(actual);
    }
}