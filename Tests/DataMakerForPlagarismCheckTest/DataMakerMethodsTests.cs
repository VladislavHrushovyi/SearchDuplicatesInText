using System.Collections.ObjectModel;
using System.Text;
using Moq;
using SearchDuplicatesText.Services.MakeDataForMethodsService;

namespace Tests.DataMakerForPlagarismCheckTest;

public class DataMakerMethodsTests
{
    private readonly Mock<ConvertTextToDataMethod> _convertText;

    public DataMakerMethodsTests()
    {
        _convertText = new Mock<ConvertTextToDataMethod>(null);
    }

    [Fact]
    public async Task CheckCreatingNgrams()
    {
        var data = new StringBuilder("cім раз відріж");
        var actual = await _convertText.Object.GetNgrams(data);

        var expected = new List<string>()
        {
            "cімразвідріж",
        }.AsReadOnly();
        
        Assert.NotNull(actual);
        Assert.NotEmpty(actual);
        Assert.Equal(expected, actual);
    }
}