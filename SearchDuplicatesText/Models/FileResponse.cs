using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.Models;

public sealed class FileResponse
{
    public NgramFile NgramFile { get; set; }
    public ShingleFile ShingleFile { get; set; }
    public ExpFile ExpFile { get; set; }

    public FileResponse(NgramFile ngram, ShingleFile shingle, ExpFile exp)
    {
        this.NgramFile = ngram;
        this.ShingleFile = shingle;
        this.ExpFile = exp;
    }
}