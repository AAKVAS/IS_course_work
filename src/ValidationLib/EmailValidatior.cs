using System.Text.RegularExpressions;

namespace ValidationLib
{
    /// <summary>
    /// Класс валидации электронной почты.
    /// </summary>
    public class EmailValidatior
    {
        /// <summary>
        /// Регулярное выражение для проверки соответствия строки электронной почте.
        /// </summary>
        private static string _emailPattern = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])$";

        /// <summary>
        /// Метод проверки строки на соответствие электронной почте. В качестве параметра принимает строку, которую нужно проверить.
        /// Если строка пустая или больше 255 символов, то она считается некорректной.
        /// </summary>
        /// <param name="str">Строка для проверки.</param>
        /// <returns>Истина, если строка является электронной почтой.</returns>
        public static bool IsValid(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length > 255)
            {
                return false;
            }
            return Regex.IsMatch(str, _emailPattern, RegexOptions.IgnoreCase);
        }
    }
}
