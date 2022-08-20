using System.Text;
using SearchDuplicatesText.DataRepositories;
using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.Services.MakeDataForMethodsService;

public class ConvertTextToDataMethod
{
    private readonly FileRepository _fileRepository;
    private readonly List<string> _hashesShingle = new();

    public ConvertTextToDataMethod(FileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    // public async Task<List<string>> GetShinglesHash(StringBuilder text)
    // {
    //     return await CreateShingleHashes(text);
    // }
    //
    // public async Task<List<string>> GetNgrams(StringBuilder text)
    // {
    //     return await CreateNgrams(text);
    // }

    public async Task<ShingleFile> CreateShingleFile(StringBuilder text, string? fileName)
    {
        int shingleCount = 0;
        using (var streamWriter = new StreamWriter($"./Files/ShingleHashFile/{fileName}", true))
        {
            await foreach (var shingle in CreateShingleHashes(text))
            {
                await streamWriter.WriteLineAsync(shingle);
                shingleCount++;
            }
        }
        var result = await _fileRepository.AddShingleFile(new ShingleFile() {Name = fileName, ShingleCount = shingleCount});
        
        return result;
    }

    public async Task<NgramFile> CreateNgramFile(StringBuilder text, string? fileName)
    {
        int ngramCount = 0;
        using (var streamWriter = new StreamWriter($"./Files/NgramFiles/{fileName}", true))
        {
            await foreach (var ngram in CreateNgrams(text))
            {
                await streamWriter.WriteLineAsync(ngram);
                ngramCount++;
            }
        }
        var result = await _fileRepository.AddNgramFile(new NgramFile() {Name = fileName, NumberOfNgram = ngramCount});

        return result;
    }

    private async IAsyncEnumerable<string> CreateNgrams(StringBuilder text, int ngramCount = 20)
    {
            text.Replace(" ", "");
            for (int i = 0; i < text.Length - ngramCount; i++)
            {
                for (int j = i; j < i + ngramCount; j++)
                {
                    yield return text[j].ToString();
                }
            }
    }

    private async IAsyncEnumerable<string> CreateShingleHashes(StringBuilder text, int numberOfShingle = 3)
    {
        var shingle = new StringBuilder();
        var splitText = text.ToString().Split(" ");
        for (int i = 0; i < (splitText.Length - numberOfShingle); i++)
        {
            yield return await shingle.ToString().StringToSha1();
        }
    }
}
