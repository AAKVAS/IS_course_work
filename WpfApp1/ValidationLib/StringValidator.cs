namespace ValidationLib
{
    public class StringValidator
    {
        public static bool IsValid(string str)
        {
            return !string.IsNullOrWhiteSpace(str) && str.Length < 256;
        }
    }
}