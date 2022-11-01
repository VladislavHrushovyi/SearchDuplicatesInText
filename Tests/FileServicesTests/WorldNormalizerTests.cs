using SearchDuplicatesText.Services.FileService;

namespace Tests.FileServicesTests;

public class WorldNormalizerTests
{
    private readonly WordNormalizer _normalizer;

    public WorldNormalizerTests()
    {
        _normalizer = new WordNormalizer();
    }

    [Fact]
    public async Task CheckIsAdjectiveWord()
    {
        string adjective = "Каштановий";
        string notAdjective = "Кокос";
        string union = "а";
        string notUnion = "Футбол";

        var actualAdjTrue = await _normalizer.IsWrongWord(adjective);
        var actualAdjFalse = await _normalizer.IsWrongWord(notAdjective);
        var actualUnionTrue = await _normalizer.IsWrongWord(union);
        var actualUnionFalse = await _normalizer.IsWrongWord(notUnion);
        
        Assert.False(actualAdjFalse);
        Assert.True(actualAdjTrue);
        Assert.False(actualUnionFalse);
        Assert.True(actualUnionTrue);
    }
}