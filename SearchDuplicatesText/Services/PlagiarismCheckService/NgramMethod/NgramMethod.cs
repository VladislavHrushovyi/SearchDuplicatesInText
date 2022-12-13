using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using SearchDuplicatesText.DataRepositories.InMemoryContext.Repositories.ProgressRepository;
using SearchDuplicatesText.DataRepositories.PostgreSqlContext.Repositories;
using SearchDuplicatesText.Models;
using SearchDuplicatesText.Models.DataBase;
using SearchDuplicatesText.Services.MakeDataForMethodsService;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.NgramMethod;

public class NgramMethod : BaseMethod, IPlagiarismMethod
{
    private readonly ConvertTextToDataMethod _convertText;

    public NgramMethod(FileRepository fileRepository,IProgressRepository progressRepository ,ConvertTextToDataMethod convertText) 
        : base(fileRepository, progressRepository)
    {
        _convertText = convertText;
    }

    public async Task<List<MethodResult>> StartMethod(ReadOnlyCollection<string> dataForMethod, string progressName)
    {
        var ngramFiles = await _fileRepository.GetFiles<NgramFile>();
        using var result = new BlockingCollection<MethodResult>();
        var watch = new Stopwatch();
        var enumerable = ngramFiles as NgramFile[] ?? ngramFiles.ToArray();
        var progress = await _progressRepository.AddProgress(new ProgressModel()
        {
            NameOfProgress = progressName,
            AllItems = enumerable.Count(),
            Progress = 0
        });
        watch.Start();
        await Parallel.ForEachAsync(enumerable,
            new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount},
            async (file, token) =>
            {
                var dataFromFile = await File.ReadAllLinesAsync($"./Files/NgramFiles/{file.Name}", token);
                var ngramMatch1 = dataForMethod.Intersect(dataFromFile).Count();
                var ngramMatch2 = dataFromFile.Intersect(dataForMethod).Count();
                var percent = Convert.ToDouble(ngramMatch1 + ngramMatch2) / Convert.ToDouble(dataForMethod.Count + file.NumberOfNgram);
                await Task.Delay(StaticData.GetRandomDelay(), token).ContinueWith(async (r) =>
                {
                    var methodResult = new MethodResult()
                    {
                        NameFile = file.Name,
                        Percent = percent * 100
                    };
                    result.Add(methodResult, token);
                    progress.Progress = result.Count;
                    await _progressRepository.UpdateProgress(progress);
                    Console.WriteLine($"Do {progress.Progress} in {progress.AllItems}");
                }, token);
                
            });
        watch.Stop();
        var ts = watch.Elapsed;

        Console.WriteLine("RunTime " + ts);
        return result.ToList();
    }

    public async Task<ReadOnlyCollection<string>> GetPreparedData(StringBuilder text) => await _convertText.GetNgrams(text);
    
}