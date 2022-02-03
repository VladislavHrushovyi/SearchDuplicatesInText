using SearchDuplicatesText.Models;

namespace SearchDuplicatesText.Services.PlagiarismCheckService;

public interface IPlagiarismMethod
{
    public Task<List<MethodResult>> StartMethod(List<string> dataForMethod);
}