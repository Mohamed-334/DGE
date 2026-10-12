using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace DGE.Service.Shared.Security
{
    /// <summary>
    /// AES-256-GCM encryption for access tokens.
    /// Payload layout (Base64): nonce (12 bytes) | tag (16 bytes) | cipher text.
    /// </summary>
    public static class TokenEncryptor
    {
        private const int KeySize = 32;
        private static readonly int NonceSize = AesGcm.NonceByteSizes.MaxSize;
        private static readonly int TagSize = AesGcm.TagByteSizes.MaxSize;

        /// <summary>Parses the hex EncryptionKey and fails fast if it is not a 256-bit key.</summary>
        public static byte[] ParseKey(string? hexKey)
        {
            byte[] key;
            try
            {
                key = Convert.FromHexString(hexKey ?? string.Empty);
            }
            catch (FormatException)
            {
                throw new InvalidOperationException("JwtSettings:EncryptionKey must be a hex string.");
            }

            if (key.Length != KeySize)
                throw new InvalidOperationException($"JwtSettings:EncryptionKey must be {KeySize * 2} hex characters (256-bit).");

            return key;
        }

        public static string Encrypt(string token, byte[] key)
        {
            var nonce = new byte[NonceSize];
            RandomNumberGenerator.Fill(nonce);
            var tag = new byte[TagSize];
            var plainText = Encoding.UTF8.GetBytes(token);
            var cipherText = new byte[plainText.Length];

            using var aes = new AesGcm(key, TagSize);
            aes.Encrypt(nonce, plainText, cipherText, tag);
            return Convert.ToBase64String([.. nonce, .. tag, .. cipherText]);
        }

        /// <summary>Returns false for malformed or tampered tokens instead of throwing.</summary>
        public static bool TryDecrypt(string? encryptedToken, byte[] key, [NotNullWhen(true)] out string? token)
        {
            token = null;
            if (string.IsNullOrWhiteSpace(encryptedToken))
                return false;

            try
            {
                var payload = Convert.FromBase64String(encryptedToken);
                if (payload.Length <= NonceSize + TagSize)
                    return false;

                var nonce = payload.AsSpan(0, NonceSize);
                var tag = payload.AsSpan(NonceSize, TagSize);
                var cipherText = payload.AsSpan(NonceSize + TagSize);
                var plainText = new byte[cipherText.Length];

                using var aes = new AesGcm(key, TagSize);
                aes.Decrypt(nonce, cipherText, tag, plainText);
                token = Encoding.UTF8.GetString(plainText);
                return true;
            }
            catch (Exception ex) when (ex is FormatException or CryptographicException)
            {
                return false;
            }
        }
    }
}
