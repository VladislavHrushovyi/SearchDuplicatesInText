using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using SearchDuplicatesText.DataRepositories;
using SearchDuplicatesText.Models;
using SearchDuplicatesText.Services.MakeDataForMethodsService;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.ShingleMethod;

public class ShingleMethod : IPlagiarismMethod
{
    private readonly FileRepository _fileRepository;
    private readonly ConvertTextToDataMethod _convertText;

    public ShingleMethod(FileRepository fileRepository, ConvertTextToDataMethod convertText)
    {
        _fileRepository = fileRepository;
        _convertText = convertText;
    }

    public async Task<List<MethodResult>> StartMethod(ReadOnlyCollection<string> dataForMethod)
    {
        var shingleFiles = await _fileRepository.GetAllShingleFile();
        var result = new BlockingCollection<MethodResult>();
        var watch = new Stopwatch();
        watch.Start();
        await Parallel.ForEachAsync(shingleFiles, new ParallelOptions(){ MaxDegreeOfParallelism = Environment.ProcessorCount},  async (file, token) =>
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
            result.Add(methodResult, token);
        });
        watch.Stop();
        var ts = watch.Elapsed;

        var elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
        Console.WriteLine("RunTime " + elapsedTime);
        return result.ToList();
    }

    public async Task<ReadOnlyCollection<string>> GetPreparedData(StringBuilder text)
    {
        return await _convertText.GetShinglesHash(text);
    }
}