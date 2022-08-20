using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.Models;

public sealed class FileResponse
{
    public NgramFile NgramFile { get; set; }
    public ShingleFile ShingleFile { get; set; }

    public FileResponse(NgramFile ngram, ShingleFile shingle)
    {
        this.NgramFile = ngram;
        this.ShingleFile = shingle;
    }
}