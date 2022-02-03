using System.Text;
using SearchDuplicatesText.DataRepositories;
using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.Services.MakeDataForMethodsService;

public class ConvertTextToDataMethod
{
    private readonly FileRepository _fileRepository;
    private readonly List<string> _hashesShingle = new();
    private readonly List<string> _ngrams = new();

    public ConvertTextToDataMethod(FileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<List<string>> GetShinglesHash(StringBuilder text, int numberOfShingles = 3)
    {
        return await CreateShingleHashes(text);
    }

    public async Task CreateShingleFile(StringBuilder text,  string? fileName, int numberOfShingle = 3)
    {
        await CreateShingleHashes(text);
        await File.WriteAllLinesAsync($"./Files/ShingleHashFile/{fileName}", _hashesShingle.Select(x => x.ToString()));
        await _fileRepository.AddShingleFile(new ShingleFile() {Name = fileName, ShingleCount = _hashesShingle.Count});
    }

    public async Task CreateNgramFile(StringBuilder text, string? fileName, int ngramCount = 10)
    {
        text.Replace(" ", "");
        var ngram = new StringBuilder();
        for (int i = 0; i < text.Length - ngramCount; i+=ngramCount)
        {
            for (int j = i; j < i + ngramCount; j++)
            {
                ngram.Append(text[j]);
            }
            _ngrams.Add(ngram.ToString());
            ngram.Clear();
        }
        await File.WriteAllLinesAsync($"./Files/NgramFiles/{fileName}", _ngrams.Select(x => x.ToString()));
        await _fileRepository.AddNgramFile(new NgramFile() {Name = fileName, NumberOfNgram = _ngrams.Count});
    }

    private async Task<List<string>> CreateShingleHashes(StringBuilder text, int numberOfShingle = 3)
    {
        return await Task.Run(async () =>
        {
            _hashesShingle.Clear();
            var shingle = new StringBuilder();
            var splitText = text.ToString().Split(" ");
            for (int i = 0; i < (splitText.Length - numberOfShingle); i++)
            {
                shingle.Append(splitText[i]).Append(splitText[i + 1]).Append(splitText[i + 2]);
                _hashesShingle.Add(await shingle.ToString().StringToSha1());
                shingle.Clear();
            }

            return _hashesShingle;
        });
    }
}