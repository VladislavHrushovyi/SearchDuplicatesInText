using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using SearchDuplicatesText.DataRepositories;
using SearchDuplicatesText.DataRepositories.InMemoryContext.Repositories.ProgressRepository;
using SearchDuplicatesText.DataRepositories.PostgreSqlContext.Repositories;
using SearchDuplicatesText.Models;
using SearchDuplicatesText.Models.DataBase;
using SearchDuplicatesText.Services.MakeDataForMethodsService;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.ExpMethod;

public class ExpMethod : BaseMethod, IPlagiarismMethod
{
    private readonly ConvertTextToDataMethod _convertText;

    public ExpMethod(FileRepository fileRepository,IProgressRepository progressRepository,ConvertTextToDataMethod convertText)
        : base(fileRepository, progressRepository)
    {
        _convertText = convertText;
    }

    public async Task<List<MethodResult>> StartMethod(ReadOnlyCollection<string> dataForMethod, string progressName)
    {
        var expFiles = await _fileRepository.GetFiles<ExpFile>();
        using var result = new BlockingCollection<MethodResult>();
        
        var watch = new Stopwatch();
        var enumerable = expFiles as ExpFile[] ?? expFiles.ToArray();
        var progress = await _progressRepository.AddProgress(new ProgressModel()
        {
            NameOfProgress = progressName,
            AllItems = enumerable.Count(),
            Progress = 0
        });
        watch.Start();

        await Parallel.ForEachAsync(enumerable, 
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
                await Task.Delay(new Random().Next(1000, 10000), token);
                result.Add(new MethodResult()
                {
                    NameFile = file.Name,
                    Percent = (amountSamePart * 100d) / (dataForMethod.Count / itemsInPart)
                });
                progress.Progress = result.Count;
                await _progressRepository.UpdateProgress(progress).ConfigureAwait(false);
                Console.WriteLine($"Do {progress.Progress} in {progress.AllItems}");
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