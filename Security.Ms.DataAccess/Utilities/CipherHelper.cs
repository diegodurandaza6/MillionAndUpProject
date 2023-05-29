using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Security.Ms.DataAccess.Utilities
{
    public class CipherHelper
    {
        private string Key { get; set; }
        private string IV { get; set; }

        public CipherHelper(string key, string iv) {
            Key = key;
            IV = iv;
        }

        /// <summary>
        /// Método que permite encriptar una cadena de texto
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>Cadena de texto cifrada</returns>
        public string Encrypt(string plainText)
        {
            byte[] Key = Encoding.UTF8.GetBytes(this.Key);
            byte[] IV = Encoding.UTF8.GetBytes(this.IV);
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
