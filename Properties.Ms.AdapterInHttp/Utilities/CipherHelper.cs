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

        public string Encrypt(string plainText)
        {
            byte[] Key = Encoding.UTF8.GetBytes(_configuration["Encrypt:Key"]);
            byte[] IV = Encoding.UTF8.GetBytes(_configuration["Encrypt:IV"]);
            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            byte[] encryptedBytes;

            using (MemoryStream ms = new())
            {
                using (CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }

            return Convert.ToBase64String(encryptedBytes);
        }
    }
}
