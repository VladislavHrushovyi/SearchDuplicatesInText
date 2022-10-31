namespace SearchDuplicatesText.Models.Responses;

public class MethodResultResponse
{
    public MethodResultResponse(IEnumerable<MethodResult> results)
    {
        MaxDuplicate = results.Max(p => p.Percent);
        Results = results.OrderByDescending(p => p.Percent);
        Count = results.Count();
    }

    public int Count { get; set; }
    public IEnumerable<MethodResult> Results { get; set; }
    public double MaxDuplicate { get; set; }
}