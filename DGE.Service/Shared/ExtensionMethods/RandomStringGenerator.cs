using System.Security.Cryptography;

public static class RandomStringGenerator
{
    private const string AllowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string Generate(int length) => RandomNumberGenerator.GetString(AllowedChars, length);
}
