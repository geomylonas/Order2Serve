using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Application.Auxiliary
{
    public class Encryption : IEncryption
    {
        private byte[]? key;
        private byte[]? iv;
        private IConfiguration _configuration;
        public Encryption(IConfiguration configuration)
        {
            _configuration = configuration;
            Initialize(_configuration["EncryptionKey"], _configuration["EncryptionIV"]);
        }

        public void Initialize(string? keyString, string? ivString)
        {
            if (keyString == null || ivString == null) throw new ArgumentNullException("Key Or IV is null!");

            key = Encoding.UTF8.GetBytes(keyString);
            iv = Encoding.UTF8.GetBytes(ivString);

            if (key.Length != 32 || iv.Length != 16) throw new ArgumentException("Invalid key or IV length.");
        }

        public string Encrypt(string plainText)
        {
            if (key == null || iv == null) throw new InvalidOperationException("Encryption class is not initialized.");

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string encryptedText)
        {
            if (key == null || iv == null) throw new InvalidOperationException("Encryption class is not initialized.");

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (MemoryStream output = new MemoryStream())
                        {
                            cs.CopyTo(output);
                            return Encoding.UTF8.GetString(output.ToArray());
                        }
                    }
                }
            }
        }
    }
}
