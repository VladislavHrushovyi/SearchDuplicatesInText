using Microsoft.AspNetCore.Mvc;
using SearchDuplicatesText.Models;

namespace SearchDuplicatesText.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingsController : ControllerBase
{
    [HttpGet("get-settings")]
    public async Task<IResult> GetInfo()
    {
        return Results.Ok(new 
        {
            ExpPart = StaticData.ExpPart,
            NgramPart = StaticData.Ngram,
            ShinglePart = StaticData.Shingle
        });
    }

    [HttpPost("update-settings")]
    public async Task<IResult> UpdateSettings([FromBody] SettingsModel settings)
    {
        StaticData.SetNewSettings(settings);
        
        return Results.Ok(new 
        {
            ExpPart = StaticData.ExpPart,
            NgramPart = StaticData.Ngram,
            ShinglePart = StaticData.Shingle
        });
    }
}