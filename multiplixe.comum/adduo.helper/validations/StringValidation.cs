using System.Text.RegularExpressions;

namespace adduo.helper.validations
{
    public class StringValidation
    {
        public static bool Test(string text)
        {
            var test = false;

            if (!string.IsNullOrEmpty(text))
            {
                string pattern = @"^[\p{L}\p{M}' \.\-]+$";

                var check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);

                test = check.IsMatch(text);
            }
            return test;
        }
    }
}
