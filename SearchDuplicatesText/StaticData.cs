using SearchDuplicatesText.Models;

namespace SearchDuplicatesText;

public class StaticData
{
    public static int ExpPart { get; set; }
    public static int Ngram { get; set; }
    public static int Shingle { get; set; }

    static StaticData()
    {
        ExpPart = 3;
        Ngram = 20;
        Shingle = 3;
    }
    public static int GetRandomDelay()
    {
        int from = 100, to = 200;
        return new Random().Next(from, to);
    }

    public static void SetNewSettings(SettingsModel settings)
    {
        Ngram = settings.NgramPart != 0 ? settings.NgramPart : Ngram;
        Shingle = settings.ShinglePart != 0 ? settings.ShinglePart : Shingle;
        ExpPart = settings.ExpPart != 0 ? settings.ExpPart : ExpPart;
    }
}