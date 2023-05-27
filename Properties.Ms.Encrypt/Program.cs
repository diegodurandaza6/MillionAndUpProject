// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;

internal class Program
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("#Bzj35mt4y9Fcm*@I&RR3lyOuadTEC#Q"); // Clave secreta para el cifrado. Debe tener 16, 24 o 32 bytes dependiendo del tamaño de clave AES seleccionado.
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("0li15F6*0v6CzZqz"); // Vector inicial para el cifrado. Debe tener 16 bytes.
    private static void Main(string[] args)
    {
        Console.Write(Encrypt("Admin123*"));
        Console.ReadLine();
    }

    private static string Encrypt(string plainText)
    {
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
