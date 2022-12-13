using System.Text;
using System.Text.RegularExpressions;

namespace SearchDuplicatesText.Services.FileService;

public class TextClearing
{
    public async Task<string?> ClearTextAsync(StringBuilder text)
    {
        //@"(?i)[^А-ЯЁІЇA-Z\s]"
        return await Task.Run( () =>
        {
            text.Replace("\t", " ").Replace("\r"," ").Replace("\n", " ").Replace("    ", " ").Replace("+"," ");
            return Regex.Replace(text.ToString(), @"(?i)[^А-ЯЁІЇЄ\s+]", "").ToLower();
        });
    }
}