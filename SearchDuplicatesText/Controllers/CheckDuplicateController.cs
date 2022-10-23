using System.Text;
using Microsoft.AspNetCore.Mvc;
using SearchDuplicatesText.Services.PlagiarismCheckService;
using SearchDuplicatesText.Services.PlagiarismCheckService.ExpMethod;
using SearchDuplicatesText.Services.PlagiarismCheckService.NgramMethod;
using SearchDuplicatesText.Services.PlagiarismCheckService.ShingleMethod;

namespace SearchDuplicatesText.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckDuplicateController : ControllerBase
{
    private readonly PlagiarismCheckService _plagiarismCheck;
    private readonly ShingleMethod _shingleMethod;
    private readonly NgramMethod _ngramMethod;
    private readonly ExpMethod _expMethod;

    public CheckDuplicateController(PlagiarismCheckService plagiarismCheck, ShingleMethod shingleMethod, ExpMethod expMethod, NgramMethod ngramMethod)
    {
        _plagiarismCheck = plagiarismCheck;
        _shingleMethod = shingleMethod;
        _expMethod = expMethod;
        _ngramMethod = ngramMethod;
    }

    [HttpPost("file/shingle-check")]
    public async Task<IResult> ShingleCheckFile(IFormFile data)
    {
        var result = await _plagiarismCheck.CheckPlagiarismByFile(data, _shingleMethod);
        return result;
    }
    
    [HttpPost("text/shingle-check")]
    public async Task<IResult> ShingleCheckText([FromForm] String data)
    {
        var result = await _plagiarismCheck.CheckPlagiarismByText(data, _shingleMethod);
        return result;
    }
    [HttpPost("file/ngram-check")]
    public async Task<IResult> NgramCheckFile(IFormFile data)
    {
        var result = await _plagiarismCheck.CheckPlagiarismByFile(data, _ngramMethod);
        return result;
    }
    [HttpPost("text/ngram-check")]
    public async Task<IResult> NgramCheckText([FromForm]String data)
    {
        var result = await _plagiarismCheck.CheckPlagiarismByText(data, _ngramMethod);
        return result;
    }
    [HttpPost("file/exp-check")]
    public async Task<IResult> ExpCheckFile(IFormFile data)
    {
        var result = await _plagiarismCheck.CheckPlagiarismByFile(data, _expMethod);
        return result;
    }
    [HttpPost("text/exp-check")]
    public async Task<IResult> ExpCheckText([FromForm]String data)
    {
        var result = await _plagiarismCheck.CheckPlagiarismByText(data, _expMethod);
        return result;
    }
}