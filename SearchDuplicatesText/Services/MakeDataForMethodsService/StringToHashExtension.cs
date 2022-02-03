using System.Security.Cryptography;
using System.Text;

namespace SearchDuplicatesText.Services.MakeDataForMethodsService;

public static class StringToHashExtension
{
    public static async Task<string> StringToSha1(this string str)
    {
        return await Task.Run(() =>
        {
            var sb = new StringBuilder();
            foreach (var b in GetHash(str))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        });
    }

    private static byte[] GetHash(string inputString)
    {
        using HashAlgorithm algorithm = SHA1.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }
}