using Application.Abstractions.ExternalService;
using System.Security.Cryptography;

namespace Infrastructure.ExternalService;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100_000;

    public string HashPassword(string password)
    {
        return Hash(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return Verify(hashedPassword, password);
    }

    public string Hash(string password)
    {
        var salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt);

        using var derive = new Rfc2898DeriveBytes(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256
        );

        var key = derive.GetBytes(KeySize);

        var output = new byte[1 + 4 + SaltSize + KeySize];

        output[0] = 1;

        BitConverter.GetBytes(Iterations).CopyTo(output, 1);
        salt.CopyTo(output, 1 + 4);
        key.CopyTo(output, 1 + 4 + SaltSize);

        return Convert.ToBase64String(output);
    }

    public bool Verify(string hashedPassword, string providedPassword)
    {
        if (string.IsNullOrEmpty(hashedPassword))
            return false;

        var data = Convert.FromBase64String(hashedPassword);

        if (data.Length < 1 + 4 + SaltSize + KeySize)
            return false;

        if (data[0] != 1)
            return false;

        var iterations = BitConverter.ToInt32(data, 1);

        var salt = new byte[SaltSize];
        Array.Copy(data, 1 + 4, salt, 0, SaltSize);

        var key = new byte[KeySize];
        Array.Copy(data, 1 + 4 + SaltSize, key, 0, KeySize);

        using var derive = new Rfc2898DeriveBytes(
            providedPassword,
            salt,
            iterations,
            HashAlgorithmName.SHA256
        );

        var keyToCheck = derive.GetBytes(KeySize);

        return CryptographicOperations.FixedTimeEquals(
            key,
            keyToCheck
        );
    }
}