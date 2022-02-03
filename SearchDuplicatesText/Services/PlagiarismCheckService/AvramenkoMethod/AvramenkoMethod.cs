using SearchDuplicatesText.Models;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.AvramenkoMethod;

public class AvramenkoMethod : IPlagiarismMethod
{
    public async Task<List<MethodResult>> StartMethod(List<string> dataForMethod)
    {
        return await Task.Run(() => new List<MethodResult>()
        {
            new MethodResult(){nameFile = "Avramenko", percent = 100}
        });
    }
}