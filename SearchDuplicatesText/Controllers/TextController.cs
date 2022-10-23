using Microsoft.AspNetCore.Mvc;
using SearchDuplicatesText.Services.FileService;

namespace SearchDuplicatesText.Controllers;

[ApiController]
[Route("[controller]")]
public class TextController
{
    private readonly UploadFileService _uploadFileService;

    public TextController(UploadFileService uploadFileService)
    {
        _uploadFileService = uploadFileService;
    }

    [HttpPost("upload-file")]
    public async Task<IResult> UploadFile(IFormFile file)
    {
        var result = await _uploadFileService.SaveUploadFile(file);
        return result;
    }

    [HttpPost("upload-text")]
    public async Task<IResult> UploadText(string text)
    {
        var result = await _uploadFileService.SaveUploadText(text);

        return result;
    }

    [HttpDelete("delete/{id}")]
    public async Task<IResult> DeleteText([FromQuery] string name)
    {
        return null;
    }
}