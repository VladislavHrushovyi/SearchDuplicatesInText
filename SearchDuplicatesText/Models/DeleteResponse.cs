using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.Models;

public sealed class DeleteResponse
{
    public NgramFile NgramFile { get; set; }
    public ShingleFile ShingleFile { get; set; }

    public DeleteResponse(NgramFile ngram, ShingleFile shingle)
    {
        this.NgramFile = ngram;
        this.ShingleFile = shingle;
    }
}