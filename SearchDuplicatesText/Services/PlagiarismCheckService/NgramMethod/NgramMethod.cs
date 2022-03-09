using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using SearchDuplicatesText.DataRepositories;
using SearchDuplicatesText.Models;
using SearchDuplicatesText.Services.MakeDataForMethodsService;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.NgramMethod;

public class NgramMethod : IPlagiarismMethod
{
    private readonly FileRepository _fileRepository;
    private readonly ConvertTextToDataMethod _convertText;

    public NgramMethod(FileRepository fileRepository, ConvertTextToDataMethod convertText)
    {
        _fileRepository = fileRepository;
        _convertText = convertText;
    }

    public async Task<List<MethodResult>> StartMethod(List<string> dataForMethod)
    {
        var ngramFiles = await _fileRepository.GetAllNgramFiles();
        using var result = new BlockingCollection<MethodResult>();
        var watch = new Stopwatch();
        watch.Start();
        await Parallel.ForEachAsync(ngramFiles,
            new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount},
            async (file, token) =>
            {
                var dataFromFile = await File.ReadAllLinesAsync($"./Files/NgramFiles/{file.Name}", token);
                var ngramMatch1 = dataForMethod.Intersect(dataFromFile).Count();
                var ngramMatch2 = dataFromFile.Intersect(dataForMethod).Count();
                var percent = Convert.ToDouble(ngramMatch1 + ngramMatch2) / Convert.ToDouble(dataForMethod.Count + file.NumberOfNgram);
                var methodResult = new MethodResult()
                {
                    NameFile = file.Name,
                    Percent = percent * 100
                };
                result.Add(methodResult, token);
            });
        watch.Stop();
        var ts = watch.Elapsed;

        var elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
        Console.WriteLine("RunTime " + elapsedTime);
        return result.ToList();
    }

    public async Task<List<string>> GetPreparedData(StringBuilder text)
    {
        return await _convertText.GetNgrams(text);
    }
}