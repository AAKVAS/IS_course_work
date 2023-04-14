using System.Text.RegularExpressions;

namespace PasswordEvaluatorLib
{
    public enum PasswordComplexity
    {
        Weak,
        Medium,
        Strong
    }

    public class PasswordEvaluator
    {
        static string patternContainsNumber = @".*\d.*";
        static string patternContainsCapitalLetter = @".*[A-Z]|[А-Я].*";
        static string patternContainsLowerLetter = @".*[a-z]|[а-я].*";
        static string patternContainsSigns = @".*\W.*";

        private string _password;
        private int _complexityPoints;

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
        
        private void IncreasePointsIfLongPassword()
        {
            if (_password.Length > 13)
            {
                _complexityPoints++;
            }
        }

        private void CheckNumberAndIncreasePoints()
        {
            CheckPatternAndIncreasePoints(patternContainsNumber);
        }
        private void CheckCapitalLetterAndIncreasePoints()
        {
            CheckPatternAndIncreasePoints(patternContainsCapitalLetter);
        }
        private void CheckLowerLetterAndIncreasePoints()
        {
            CheckPatternAndIncreasePoints(patternContainsLowerLetter);
        }
        private void CheckSignAndIncreasePoints()
        {
            CheckPatternAndIncreasePoints(patternContainsSigns);
        }
        private void CheckPatternAndIncreasePoints(string pattern)
        {
            if (Regex.IsMatch(_password, pattern))
            {
                _complexityPoints++;
            }
        }

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