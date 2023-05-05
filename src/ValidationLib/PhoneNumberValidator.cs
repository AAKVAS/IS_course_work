using System.Text.RegularExpressions;

namespace ValidationLib
{
    /// <summary>
    /// Класс валидации номера телефона.
    /// </summary>
    public class PhoneNumberValidator
    {
        /// <summary>
        /// Регулярное выражение для проверки соответствия строки номеру телефона.
        /// </summary>
        private static string _phoneNumberPattern = @"^(\+?\d+[ -]?)?(\(\d+\)[ -]?)?(\d+[ -]?)*$";

        /// <summary>
        /// Метод проверки строки на соответствие номеру телефона. В качестве параметра принимает строку, которую нужно проверить.
        /// Если строка пустая или больше 255 символов, то она считается некорректной.
        /// </summary>
        /// <param name="str">Строка для проверки.</param>
        /// <returns>Истина, если строка является номером телефона.</returns>
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
