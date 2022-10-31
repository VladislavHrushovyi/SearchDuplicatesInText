using Microsoft.AspNetCore.Mvc;
using SearchDuplicatesText.DataRepositories.InMemoryContext.Repositories.ProgressRepository;
using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.Controllers;

[ApiController]
[Route("[controller]")]
public class ProgressController : ControllerBase
{
    private readonly IProgressRepository _progressRepository;

    public ProgressController(IProgressRepository progressRepository)
    {
        _progressRepository = progressRepository;
    }

    [HttpGet("{name}")]
    public async Task<IResult> GetProgress(string name)
    {
        Console.WriteLine(name);
        var result = await _progressRepository.GetProgressByName(name);
        if(result == default) return Results.Ok(new ProgressModel());
        return Results.Ok(result);
    }
}