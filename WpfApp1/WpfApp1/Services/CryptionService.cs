using System.Security.Cryptography;
using System.Text;


namespace WpfApp1.Services
{
    /// <summary>
    /// Класс, обеспечивающий шифрование
    /// </summary>
    public class CryptionService
    {
        /// <summary>
        /// Метод, создающий хэш строки по алгоритму SHA 256.
        /// </summary>
        /// <param name="str">Строка для хэширования.</param>
        /// <returns>Хэш строки в виде массива байтов.</returns>
        public static byte[] HashSHA256(string str)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
                return hashValue;
            }
        }
    }
}
