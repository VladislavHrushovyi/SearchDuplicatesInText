using SearchDuplicatesText.Models;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.NgramMethod;

public class NgramMethod : IPlagiarismMethod
{
    public async Task<List<MethodResult>> StartMethod(List<string> dataForMethod)
    {
        return await Task.Run(() => new List<MethodResult>()
        {
            new MethodResult(){nameFile = "Ngram", percent = 100}
        });
    }
}