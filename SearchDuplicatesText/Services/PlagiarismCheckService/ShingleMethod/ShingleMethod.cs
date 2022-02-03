using SearchDuplicatesText.DataRepositories;
using SearchDuplicatesText.Models;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.ShingleMethod;

public class ShingleMethod : IPlagiarismMethod
{
    private readonly FileRepository _fileRepository;

    public ShingleMethod(FileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<List<MethodResult>> StartMethod(List<string> dataForMethod)
    {
        var shingleFiles = await _fileRepository.GetAllShingleFile();
        var result = new List<MethodResult>();
        Parallel.ForEachAsync(shingleFiles,new ParallelOptions(){MaxDegreeOfParallelism = 2} ,  async (file, token) =>
        {
            Console.WriteLine(token.IsCancellationRequested);
            var compareFile = await File.ReadAllLinesAsync($"./Files/ShingleHashFile/{file.Name}", token);
            var commonShingleCount = dataForMethod.Intersect(compareFile).Count();
            var methodResult = new MethodResult()
            {
                nameFile = file.Name,
                percent = commonShingleCount == 0
                    ? 0
                    : (Convert.ToDouble(commonShingleCount) / Convert.ToDouble(compareFile.Length)) * 100

            };
            result.Add(methodResult);
        }).Wait();

        return result;
    }
}