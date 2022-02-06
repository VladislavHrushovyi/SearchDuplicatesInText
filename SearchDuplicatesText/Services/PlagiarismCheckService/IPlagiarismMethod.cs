using System.Text;
using SearchDuplicatesText.Models;

namespace SearchDuplicatesText.Services.PlagiarismCheckService;

public interface IPlagiarismMethod
{
    public Task<List<MethodResult>> StartMethod(List<string> dataForMethod);
    public Task<List<string>> GetPreparedData(StringBuilder text);
}