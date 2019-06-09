using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ion.IR.Syntax;

namespace Ion.IR.Constants
{
    public static class TokenConstants
    {
        public static Dictionary<string, TokenType> symbols = new Dictionary<string, TokenType>
        {
            {"@", TokenType.SymbolAt},
            {":", TokenType.SymbolColon},
            {"$", TokenType.SymbolDollar},
            {"#", TokenType.SymbolHash}
        };

        public static Dictionary<string, TokenType> instructions = new Dictionary<string, TokenType>
        {
            {InstructionName.Call, TokenType.InstructionCall},
            {InstructionName.End, TokenType.InstructionEnd},
            {InstructionName.Create, TokenType.InstructionCreate},
            {InstructionName.Set, TokenType.InstructionSet}
        };

        public static readonly Dictionary<Regex, TokenType> complex = new Dictionary<Regex, TokenType>
        {
            {Pattern.Identifier, TokenType.Identifier},
            {Pattern.String, TokenType.LiteralString},
            {Pattern.Decimal, TokenType.LiteralDecimal},
            {Pattern.Integer, TokenType.LiteralNumber},
            {Pattern.Character, TokenType.LiteralCharacter}
        };
    }
}
