using Ecommerce.BuildingBlocks.Domain;
using ErrorOr;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.UserAccess.Domain.Common;

public sealed record Password : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    public static Password? Create(string value)
    {
        if (value.Length < 0)
        {
            return null;
        }

        string passwordHash = GenerateHash256(value);
        return new Password(passwordHash);
    }

    private static string GenerateHash256(string passwordValue)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordValue));

        var passwordBuilder = new StringBuilder();

        foreach (var s in bytes)
        {
            passwordBuilder.Append(s.ToString());
        }

        return passwordBuilder.ToString();
    }

    private Password(string value)
    {
        Value = value;
    }

    private Password() { }
}
