using System.Text;
using SearchDuplicatesText.Services.FileService;

namespace Tests.FileServicesTests;

public class TextClearingTests
{
    private readonly TextClearing _textClearing;

    public TextClearingTests()
    {
        _textClearing = new TextClearing();
    }

    [Fact]
    public async Task CheckClearedText()
    {
        StringBuilder data = new StringBuilder(@"123 asdqало ало ало    ???>>>>}}}{}+_)((&^%$#@@!@?,.!@#$%^&*()_+|\||\\//?><~~");

        var actual = await _textClearing.ClearTextAsync(data);
        
        Assert.Equal(" ало ало ало", actual);
        Assert.NotNull(actual);
    }
}