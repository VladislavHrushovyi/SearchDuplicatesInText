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
                var itemsInPart = StaticData.ExpPart;
                var dataFromFile = await File.ReadAllLinesAsync($"./Files/ExpFiles/{file.Name}", token);
                double expMatch = CalculateSamePart(dataForMethod, dataFromFile, (int)itemsInPart);
                double percent = (expMatch ) / (dataForMethod.Count - itemsInPart + 1);
                await Task.Delay(StaticData.GetRandomDelay(), token).ContinueWith(async (r) =>
                {
                    result.Add(new MethodResult()
                    {
                        NameFile = file.Name,
                        Percent = percent * 100
                    }, token);
                    Console.WriteLine($"Do {progress.Progress} in {progress.AllItems}");
                    progress.Progress = result.Count;
                    await _progressRepository.UpdateProgress(progress);
                }, token);
            });
        
        watch.Stop();
        var ts = watch.Elapsed;

        Console.WriteLine("RunTime " + ts);

        return result.ToList();
    }
    
    private int CalculateSamePart(IEnumerable<string> firstFile, IEnumerable<string> secondFile, int expPart)
    {
        int sameCount = 0;
        for (int ch1 = 0; ch1 < firstFile.Count() - expPart + 1; ch1++)
        {
            var part1 = firstFile.Skip(ch1).Take(expPart);
            for (int ch2 = 0; ch2 < secondFile.Count() - expPart + 1; ch2++)
            {
                var part2 = secondFile.Skip(ch2).Take(expPart);
                if (part1.SequenceEqual(part2))
                {
                    sameCount++;
                    break;
                }
            }
        }

        return sameCount;
    }

    public async Task<ReadOnlyCollection<string>> GetPreparedData(StringBuilder text) => await _convertText.GetExps(text);

}