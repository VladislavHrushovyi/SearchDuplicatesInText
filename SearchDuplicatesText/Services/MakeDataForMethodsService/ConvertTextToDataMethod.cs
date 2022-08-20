using System.Collections.ObjectModel;
using System.Text;
using SearchDuplicatesText.DataRepositories;
using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.Services.MakeDataForMethodsService;

public class ConvertTextToDataMethod
{
    private readonly FileRepository _fileRepository;

    public ConvertTextToDataMethod(FileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<ReadOnlyCollection<string>> GetShinglesHash(StringBuilder text)
    {
        List<string> hashesFromUserFile = new();
        await foreach (var line in CreateShingleHashes(text))
        {
            hashesFromUserFile.Add(line);
        }

        return hashesFromUserFile.AsReadOnly();
    }
    
    public async Task<ReadOnlyCollection<string>> GetNgrams(StringBuilder text)
    {
        List<string> ngramsFromUserFile = new();
        await foreach (var line in CreateNgrams(text))
        {
            ngramsFromUserFile.Add(line);
        }

        return ngramsFromUserFile.AsReadOnly();
    }

    public async Task<ShingleFile> CreateShingleFile(StringBuilder text, string? fileName)
    {
        int shingleCount = 0;
        using (var streamWriter = new StreamWriter($"./Files/ShingleHashFile/{fileName}", true))
        {
            await foreach (var shingle in CreateShingleHashes(text).ConfigureAwait(false))
            {
                await streamWriter.WriteLineAsync(shingle);
                shingleCount++;
            }
        }

        var result =
            await _fileRepository.AddShingleFile(new ShingleFile() {Name = fileName, ShingleCount = shingleCount});

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
            yield return text.ToString(i, ngramCount);
        }
    }

    private async IAsyncEnumerable<string> CreateShingleHashes(StringBuilder text, int numberOfShingle = 3)
    {
        var splitText = text.ToString().Split(" ");
        for (int i = 0; i < (splitText.Length - numberOfShingle); i++)
        {
            var shingleText = splitText.Skip(i).Take(numberOfShingle);
            yield return await string.Join("", shingleText).StringToSha1();
        }
    }
}