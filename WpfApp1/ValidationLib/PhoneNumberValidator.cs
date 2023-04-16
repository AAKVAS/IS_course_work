using System.Text.RegularExpressions;

namespace ValidationLib
{
    public class PhoneNumberValidator
    {
        private static string _phoneNumberPattern = @"^(\+?\d+[ -]?)?(\(\d+\)[ -]?)?(\d+[ -]?)*$";

        public static bool IsValid(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length > 255)
            {
                return false;
            }

            return Regex.IsMatch(str, _phoneNumberPattern);
        }
    }
}
