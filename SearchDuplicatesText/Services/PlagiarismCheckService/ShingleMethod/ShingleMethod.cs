using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using SearchDuplicatesText.DataRepositories.InMemoryContext.Repositories.ProgressRepository;
using SearchDuplicatesText.DataRepositories.PostgreSqlContext.Repositories;
using SearchDuplicatesText.Models;
using SearchDuplicatesText.Models.DataBase;
using SearchDuplicatesText.Services.MakeDataForMethodsService;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.ShingleMethod;

public class ShingleMethod : BaseMethod, IPlagiarismMethod
{
    private readonly ConvertTextToDataMethod _convertText;

    public ShingleMethod(FileRepository fileRepository,IProgressRepository progressRepository,  ConvertTextToDataMethod convertText) 
        : base(fileRepository, progressRepository)
    {
        _convertText = convertText;
    }

    public async Task<List<MethodResult>> StartMethod(ReadOnlyCollection<string> dataForMethod, string progressName)
    {
        var shingleFiles = await _fileRepository.GetFiles<ShingleFile>();
        var result = new BlockingCollection<MethodResult>();
        var watch = new Stopwatch();
        var enumerable = shingleFiles as ShingleFile[] ?? shingleFiles.ToArray();
        var progress = await _progressRepository.AddProgress(new ProgressModel()
        {
            NameOfProgress = progressName,
            AllItems = enumerable.Length,
            Progress = 0
        });
        watch.Start();
        await Parallel.ForEachAsync(enumerable, new ParallelOptions(){ MaxDegreeOfParallelism = Environment.ProcessorCount},  async (file, token) =>
        {
            var compareFile = await File.ReadAllLinesAsync($"./Files/ShingleHashFile/{file.Name}", token);
            var commonShingleCount = dataForMethod.Intersect(compareFile).Count();
            var methodResult = new MethodResult()
            {
                NameFile = file.Name,
                Percent = commonShingleCount == 0
                    ? 0
                    : (Convert.ToDouble(commonShingleCount) / Convert.ToDouble(compareFile.Length)) * 100
        
            };
            await Task.Delay(new Random().Next(1000, 10000), token);
            result.Add(methodResult, token);
            progress.Progress = result.Count();
            await _progressRepository.UpdateProgress(progress).ConfigureAwait(false);
            Console.WriteLine($"Do {progress.Progress} in {progress.AllItems}");
        });
        watch.Stop();
        var ts = watch.Elapsed;

        Console.WriteLine("RunTime " + ts);
        return result.ToList();
    }

    public async Task<ReadOnlyCollection<string>> GetPreparedData(StringBuilder text)
    {
        return await _convertText.GetShinglesHash(text);
    }
}