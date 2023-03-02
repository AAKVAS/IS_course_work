using System.Security.Cryptography;
using System.Text;


namespace WpfApp1.Services
{
    public class CryptionService
    {
        public static byte[] hashSHA255(string str)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
                return hashValue;
            }
        }
    }
}
