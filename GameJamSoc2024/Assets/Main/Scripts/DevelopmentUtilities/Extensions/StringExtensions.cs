using System.Text.RegularExpressions;

namespace _Main.Scripts.DevelopmentUtilities.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string p_string)
        {
            return string.IsNullOrWhiteSpace(p_string);
        }

        private const string EmailRegexPattern =
            @"^(?("")("".+?(?<!\)""@)|(([0-9a-z]((.(?!.))|[-!#$%&'*+/=?^`{}|~\w]))(?<=[0-9a-z])@))" +
            @"(?([)([(\d{1,3}.){3}\d{1,3}])|(([0-9a-z][-\w][0-9a-z]*.)+[a-z0-9][-a-z0-9]{0,22}[a-z0-9]))$";

        public static bool IsValidEmail(this string p_email)
        {
            if (string.IsNullOrWhiteSpace(p_email))
                return false;

            return Regex.IsMatch(p_email, EmailRegexPattern, RegexOptions.IgnoreCase);
        }
        
        public static bool IsNullOrEmpty(this string p_value) => string.IsNullOrEmpty(p_value);
    }
}