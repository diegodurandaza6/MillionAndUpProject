using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Properties.Ms.AdapterInHttp.Utilities
{
    public class CipherHelper
    {
        private readonly IConfiguration _configuration;

        public CipherHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Decrypt(string cipherText)
        {
            byte[] Key = Encoding.UTF8.GetBytes(_configuration["Encrypt:Key"]);
            byte[] IV = Encoding.UTF8.GetBytes(_configuration["Encrypt:IV"]);
            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            byte[] encryptedBytes = Convert.FromBase64String(cipherText);
            byte[] decryptedBytes;

            using (MemoryStream ms = new(encryptedBytes))
            {
                using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using StreamReader reader = new(cs);
                decryptedBytes = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
