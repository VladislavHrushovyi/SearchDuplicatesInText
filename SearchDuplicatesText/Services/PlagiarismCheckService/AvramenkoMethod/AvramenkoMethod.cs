using System.Text;
using SearchDuplicatesText.Models;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.AvramenkoMethod;

public class AvramenkoMethod : IPlagiarismMethod
{
    public async Task<List<MethodResult>> StartMethod(List<string> dataForMethod)
    {
        return await Task.Run(() => new List<MethodResult>()
        {
            new MethodResult(){NameFile = dataForMethod[0], Percent = 100}
        });
    }

    public Task<List<string>> GetPreparedData(StringBuilder text)
    {
        throw new NotImplementedException();
    }
}