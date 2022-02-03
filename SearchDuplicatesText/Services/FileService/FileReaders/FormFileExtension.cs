namespace SearchDuplicatesText.Services.FileService.FileReaders;

public static class FormFileExtension
{
    public static async Task<byte[]> GetBytes(this IFormFile file)
    {
        var memoryStream = new MemoryStream();
        await using (memoryStream.ConfigureAwait(false))
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}