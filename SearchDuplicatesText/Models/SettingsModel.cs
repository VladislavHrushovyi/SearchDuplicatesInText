namespace SearchDuplicatesText.Models;

public class SettingsModel
{
    public int ExpPart { get; set; } = StaticData.ExpPart;
    public int ShinglePart { get; set; } = StaticData.Shingle;
    public int NgramPart { get; set; } = StaticData.Ngram;
}