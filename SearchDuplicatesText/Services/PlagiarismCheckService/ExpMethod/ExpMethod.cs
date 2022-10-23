using System.Collections.ObjectModel;
using System.Text;
using SearchDuplicatesText.Models;

namespace SearchDuplicatesText.Services.PlagiarismCheckService.ExpMethod;

public class AvramenkoMethod : IPlagiarismMethod
{
    public async Task<List<MethodResult>> StartMethod(ReadOnlyCollection<string> dataForMethod)
    {
        return await Task.Run(() => new List<MethodResult>()
        {
            new MethodResult(){NameFile = dataForMethod[0], Percent = 100}
        });
    }

    public Task<ReadOnlyCollection<string>> GetPreparedData(StringBuilder text)
    {
        throw new NotImplementedException();
    }
}