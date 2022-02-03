using System.Text;
using DocumentFormat.OpenXml.Packaging;

namespace SearchDuplicatesText.Services.FileService.FileReaders;
 
 public class WordFileReader : IFileReader
 {
     public IFormFile File { get; }
     public WordFileReader(IFormFile file) => File = file;
     public async Task<StringBuilder> GetDataAsync()
     {
       return await Task.Run(() =>
         {
             using var wordFile = WordprocessingDocument.Open(File.OpenReadStream(), false);
             var text = wordFile.MainDocumentPart?.Document.Body?.InnerText;
             return new StringBuilder(text);
         }); 
     }
 }