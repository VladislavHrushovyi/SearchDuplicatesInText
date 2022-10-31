using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Http;

namespace Tests.Extension;

public static class IFormFileExtension
{
    public static IFormFile FileStreamToFormFile(this FileStream file)
    {
        var ms = new MemoryStream();
        try
        {
            file.CopyTo(ms);
            return new FormFile(ms, 0, ms.Length, file.Name, file.Name);
        }
        catch(Exception e){
            ms.Dispose();
            throw;
        }
        finally
        {
            ms.Dispose();
        }
    }

    public static IFormFile ConvertToFormFile(this FileStream stream)
    {
        return  new FormFile(stream, 0, stream.Length, stream.Name, Path.GetFileName(stream.Name))
        {
             Headers = new HeaderDictionary(),
             ContentType = "application/pdf"
         };
    }
}