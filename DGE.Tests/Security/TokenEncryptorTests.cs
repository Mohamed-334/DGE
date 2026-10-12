using DGE.Service.Shared.Security;
using System.Security.Cryptography;

namespace DGE.Tests.Security
{
    public class TokenEncryptorTests
    {
        private static readonly byte[] Key = TokenEncryptor.ParseKey(Convert.ToHexString(RandomNumberGenerator.GetBytes(32)));

        [Fact]
        public void Encrypt_ThenTryDecrypt_ReturnsOriginalToken()
        {
            var encrypted = TokenEncryptor.Encrypt("header.payload.signature", Key);

            Assert.True(TokenEncryptor.TryDecrypt(encrypted, Key, out var token));
            Assert.Equal("header.payload.signature", token);
        }

        [Fact]
        public void Encrypt_SameTokenTwice_ProducesDifferentCipherTexts()
        {
            Assert.NotEqual(TokenEncryptor.Encrypt("token", Key), TokenEncryptor.Encrypt("token", Key));
        }

        [Fact]
        public void TryDecrypt_TamperedPayload_ReturnsFalse()
        {
            var payload = Convert.FromBase64String(TokenEncryptor.Encrypt("token", Key));
            payload[^1] ^= 0xFF;

            Assert.False(TokenEncryptor.TryDecrypt(Convert.ToBase64String(payload), Key, out var token));
            Assert.Null(token);
        }

        [Fact]
        public void TryDecrypt_WithDifferentKey_ReturnsFalse()
        {
            var otherKey = RandomNumberGenerator.GetBytes(32);

            Assert.False(TokenEncryptor.TryDecrypt(TokenEncryptor.Encrypt("token", Key), otherKey, out _));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("not-base64!")]
        [InlineData("AAAA")]
        public void TryDecrypt_MalformedInput_ReturnsFalse(string? encrypted)
        {
            Assert.False(TokenEncryptor.TryDecrypt(encrypted, Key, out _));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("zz")]
        [InlineData("00112233")]
        public void ParseKey_InvalidKey_Throws(string? hexKey)
        {
            Assert.Throws<InvalidOperationException>(() => TokenEncryptor.ParseKey(hexKey));
        }
    }
}
