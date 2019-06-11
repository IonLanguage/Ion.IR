using System.Text.RegularExpressions;
using Ion.Engine.Constants;
using Ion.IR.Syntax;

namespace Ion.IR.Cognition
{
    public static class Recognition
    {
        public static bool DoesMatch(Regex regex, Token token)
        {
            return regex.IsMatch(token.Value);
        }

        public static bool IsInteger(Token token)
        {
            return Recognition.DoesMatch(Pattern.Integer, token);
        }

        public static bool IsDecimal(Token token)
        {
            return Recognition.DoesMatch(Pattern.Decimal, token);
        }

        public static bool IsStringLiteral(Token token)
        {
            return Recognition.DoesMatch(Pattern.String, token);
        }

        public static bool IsCharLiteral(Token token)
        {
            return Recognition.DoesMatch(Pattern.Character, token);
        }

        public static bool IsLiteral(Token token)
        {
            return Recognition.IsDecimal(token)
            || Recognition.IsInteger(token)
            || Recognition.IsStringLiteral(token);
        }
    }
}
