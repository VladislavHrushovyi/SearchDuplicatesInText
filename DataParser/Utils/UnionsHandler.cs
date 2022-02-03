namespace DataParser.Utils;

public class UnionsHandler
{
    public void HandlerUnions()
    {
        var lines = File.ReadAllLines(@"Unions.txt").ToList();
        lines = lines.Distinct().ToList();
        using TextWriter tw = new StreamWriter("Unions.txt", false);
        tw.WriteLine(lines.Count);
        foreach (string s in lines)
            tw.WriteLine(s);
    }
}