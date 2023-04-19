namespace ValidationLib
{
    /// <summary>
    /// Класс проверки корректности строки.
    /// </summary>
    public class StringValidator
    {
        /// <summary>
        /// Метод проверки корректности строки.
        /// Если строка пустая или больше 255 символов, то она считается некорректной.
        /// </summary>
        /// <param name="str">Строка для проверки.</param>
        /// <returns>Истина, если строка не пустая и её длина менее 256 символов.</returns>
        public static bool IsValid(string str)
        {
            return !string.IsNullOrWhiteSpace(str) && str.Length < 256;
        }
    }
}