namespace SearchDuplicatesText.Services.FileService;

public class WordNormalizer
{
    private readonly List<string> _adjectives = new() {"ий", "ого", "их", "им", "ої", "та", "ні", "ими"};
    private readonly List<string> _unions;

    public WordNormalizer()
    {
        _unions = File.ReadAllLines("Unions.txt").ToList();
        //this._adjectives = File.ReadAllLines("Adjective.txt").ToList();
    }

    public async Task<bool> IsWrongWord(string word)
    {
        return await IsAdjective(word) || await IsUnion(word);
    }

    private async Task<bool> IsAdjective(string word)
    {
        if (word.Length <= 3) return false;

        return await Task.Run(() =>
        {
            return _adjectives.Any(endAdj => endAdj == word.Remove(0, word.Length - endAdj.Length));
        });
    }

    private async Task<bool> IsUnion(string word)
    {
        return await Task.Run(() => _unions.Contains(word));
    }
}