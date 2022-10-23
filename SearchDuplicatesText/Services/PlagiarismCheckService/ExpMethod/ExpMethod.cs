using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using SearchDuplicatesText.DataRepositories;
using SearchDuplicatesText.Models;
using SearchDuplicatesText.Models.DataBase;
using SearchDuplicatesText.Services.MakeDataForMethodsService;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.ExpMethod;

public class ExpMethod : IPlagiarismMethod
{
    private readonly FileRepository _fileRepository;
    private readonly ConvertTextToDataMethod _convertText;

    public ExpMethod(FileRepository fileRepository, ConvertTextToDataMethod convertText)
    {
        _fileRepository = fileRepository;
        _convertText = convertText;
    }

    public async Task<List<MethodResult>> StartMethod(ReadOnlyCollection<string> dataForMethod)
    {
        var expFiles = await _fileRepository.GetFiles<ExpFile>();
        using var result = new BlockingCollection<MethodResult>();
        
        var watch = new Stopwatch();
        watch.Start();

        await Parallel.ForEachAsync(expFiles, 
            new ParallelOptions(){MaxDegreeOfParallelism = Environment.ProcessorCount},
            async (file, token) =>
            {
                var itemsInPart = 5d;
                var amountSamePart = 0;
                var dataFromFile = await File.ReadAllLinesAsync($"./Files/ExpFiles/{file.Name}", token);
                foreach (var partOfFile in dataFromFile.Chunk((int)itemsInPart))
                {
                    if (IsSubarray(dataForMethod, partOfFile, dataForMethod.Count, partOfFile.Length))
                    {
                        amountSamePart++;
                    }
                }
                
                result.Add(new MethodResult()
                {
                    NameFile = file.Name,
                    Percent = (amountSamePart * 100d) / (dataForMethod.Count / itemsInPart)
                });
            });
        
        watch.Stop();
        var ts = watch.Elapsed;

        Console.WriteLine("RunTime " + ts);

        return result.ToList();
    }

    private bool IsSubarray(ReadOnlyCollection<string> A, string[] B, int n, int m)
    {
        int i = 0, j = 0;
        while (i < n && j < m)
        {
            if (A[i] == B[j])
            {
      
                i++;
                j++;
                if (j == m)
                    return true;
            }
            else
            {
                i = i - j + 1;
                j = 0;
            }
        }
        return false;
    }

    public async Task<ReadOnlyCollection<string>> GetPreparedData(StringBuilder text) => await _convertText.GetExps(text);

}