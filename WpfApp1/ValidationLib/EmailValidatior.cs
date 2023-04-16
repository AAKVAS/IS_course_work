using System.Text.RegularExpressions;

namespace ValidationLib
{
    public class EmailValidatior
    {
        private static string _emailPattern = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])$";

        public static bool IsValid(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length > 255)
            {
                return false;
            }

            return Regex.IsMatch(str, _emailPattern);
        }
    }
}
