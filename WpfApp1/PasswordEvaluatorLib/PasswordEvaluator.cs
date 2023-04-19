using System.Text.RegularExpressions;

namespace PasswordEvaluatorLib
{
    /// <summary>
    /// Перечисление, содержащее виды сложности пароля.
    /// Виды сложности пароля: слабый, средний, сильный.
    /// </summary>
    public enum PasswordComplexity
    {
        Weak,
        Medium,
        Strong
    }

    /// <summary>
    /// Класс, оценивающий сложность пароля.
    /// </summary>
    public class PasswordEvaluator
    {        
        /// <summary>
        /// Регулярное выражение, проверяющее есть ли хотя бы одна цифра в строке.
        /// </summary>
        private static string patternContainsNumber = @".*\d.*";

        /// <summary>
        /// Регулярное выражение, проверяющее есть ли хотя бы одна прописная буква в строке.
        /// </summary>
        private static string patternContainsCapitalLetter = @".*[A-Z]|[А-Я].*";

        /// <summary>
        /// Регулярное выражение, проверяющее есть ли хотя бы одна строчная буква в строке.
        /// </summary>
        private static string patternContainsLowerLetter = @".*[a-z]|[а-я].*";

        /// <summary>
        /// Регулярное выражение, проверяющее есть ли в строке хотя бы один знак, не являющийся буквой, или цифрой.
        /// </summary>
        private static string patternContainsSigns = @".*\W.*";

        /// <summary>
        /// Оцениваемый пароль.
        /// </summary>
        private string _password;

        /// <summary>
        /// Числовая оценка сложности пароля.
        /// </summary>
        private int _complexityPoints;

        /// <summary>
        /// Метод, оценивающий сложность пароля. В качестве параметра принимает пароль.
        /// Если длина пароля меньше 8 символов, то он считается слабым.
        /// </summary>
        /// <param name="password">Пароль, который нужно оценить.</param>
        /// <returns>Сложность пароля.</returns>
        public PasswordComplexity EvaluatePassword(string password)
        {
            _complexityPoints = 1;
            _password = password;
            if (_password.Length < 8)
            {
                return PasswordComplexity.Weak;
            }
            IncreasePointsIfLongPassword();
            CheckNumberAndIncreasePoints();
            CheckCapitalLetterAndIncreasePoints();
            CheckLowerLetterAndIncreasePoints();
            CheckSignAndIncreasePoints();

            return DeterminePasswordComplexity();
        }
        
        /// <summary>
        /// Метод, повышающий сложность пароля, если его длина больше 13 символов.
        /// </summary>
        private void IncreasePointsIfLongPassword()
        {
            if (_password.Length > 13)
            {
                _complexityPoints++;
            }
        }

        /// <summary>
        /// Метод, повышающий сложность пароля, если он содержит хотя бы одну цифру.
        /// </summary>
        private void CheckNumberAndIncreasePoints()
        {
            CheckPatternAndIncreasePoints(patternContainsNumber);
        }

        /// <summary>
        /// Метод, повышающий сложность пароля, если он содержит хотя бы одну прописную букву.
        /// </summary>
        private void CheckCapitalLetterAndIncreasePoints()
        {
            CheckPatternAndIncreasePoints(patternContainsCapitalLetter);
        }

        /// <summary>
        /// Метод, повышающий сложность пароля, если он содержит хотя бы одну строчную букву.
        /// </summary>
        private void CheckLowerLetterAndIncreasePoints()
        {
            CheckPatternAndIncreasePoints(patternContainsLowerLetter);
        }

        /// <summary>
        /// Метод, повышающий сложность пароля, если он содержит хотя бы один знак, не являющийся буквой, или цифрой.
        /// </summary>
        private void CheckSignAndIncreasePoints()
        {
            CheckPatternAndIncreasePoints(patternContainsSigns);
        }

        /// <summary>
        /// Метод повышающий сложность пароля, если он содержит передаваемое регулярное выражение.
        /// </summary>
        /// <param name="pattern">Регулярное выражение, которое нужно найти в пароле.</param>
        private void CheckPatternAndIncreasePoints(string pattern)
        {
            if (Regex.IsMatch(_password, pattern))
            {
                _complexityPoints++;
            }
        }

        /// <summary>
        /// Метод, оценивающий сложность пароля на основе числовой оценки пароля.
        /// </summary>
        /// <returns></returns>
        private PasswordComplexity DeterminePasswordComplexity()
        {
            if (_complexityPoints < 4)
            {
                return PasswordComplexity.Weak;
            }
            if (_complexityPoints == 4)
            {
                return PasswordComplexity.Medium;
            }
            else
            {
                return PasswordComplexity.Strong;
            }
        }

        /// <summary>
        /// Метод, возвращающий русское название сложности пароля. Принимает в качестве параметра сложность пароля в виде объекта перечисления PasswordComplexity.
        /// </summary>
        /// <param name="passwordComplexity">Сложность пароля.</param>
        /// <returns>Русское название сложности пароля.</returns>
        public static string PasswordComplexityToRus(PasswordComplexity passwordComplexity)
        {
            switch (passwordComplexity)
            {
                case PasswordComplexity.Weak:
                    return "слабый";
                case PasswordComplexity.Medium:
                    return "средний";
                case PasswordComplexity.Strong:
                    return "сильный";
                default: 
                    return "слабый";
            }
        }
    }
}