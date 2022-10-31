using System.Collections.ObjectModel;
using System.Text;
using SearchDuplicatesText.Models;

namespace SearchDuplicatesText.Services.PlagiarismCheckService;

public interface IPlagiarismMethod
{
    public Task<List<MethodResult>> StartMethod(ReadOnlyCollection<string> dataForMethod, string progressName);
    public Task<ReadOnlyCollection<string>> GetPreparedData(StringBuilder text);
}