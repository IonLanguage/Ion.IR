using System.Text.RegularExpressions;
using Ion.Engine.Constants;
using Ion.IR.Syntax;

namespace Ion.IR.Cognition
{
    public static class Recognition
    {
        public static bool DoesMatch(Regex regex, string value)
        {
            return regex.IsMatch(value);
        }

        public static bool DoesMatch(Regex regex, Token token)
        {
            return Recognition.DoesMatch(regex, token.Value);
        }

        public static bool IsInteger(string value)
        {
            return Recognition.DoesMatch(Pattern.Integer, value);
        }

        public static bool IsInteger(Token token)
        {
            return Recognition.IsInteger(token.Value);
        }

        public static bool IsDecimal(string value)
        {
            return Recognition.DoesMatch(Pattern.Decimal, value);
        }

        public static bool IsDecimal(Token token)
        {
            return Recognition.IsDecimal(token.Value);
        }

        public static bool IsNumeric(string value)
        {
            return Recognition.IsInteger(value) || Recognition.IsDecimal(value);
        }

        public static bool IsNumeric(Token token)
        {
            return Recognition.IsNumeric(token.Value);
        }

        public static bool IsStringLiteral(string value)
        {
            return Recognition.DoesMatch(Pattern.String, value);
        }

        public static bool IsStringLiteral(Token token)
        {
            return Recognition.IsStringLiteral(token.Value);
        }

        public static bool IsCharLiteral(string value)
        {
            return Recognition.DoesMatch(Pattern.Character, value);
        }

        public static bool IsCharLiteral(Token token)
        {
            return Recognition.DoesMatch(Pattern.Character, token);
        }

        public static bool IsLiteral(string value)
        {
            return Recognition.IsStringLiteral(value)
                || Recognition.IsInteger(value)
                || Recognition.IsStringLiteral(value);
        }

        public static bool IsLiteral(Token token)
        {
            return Recognition.IsLiteral(token.Value);
        }
    }
}
