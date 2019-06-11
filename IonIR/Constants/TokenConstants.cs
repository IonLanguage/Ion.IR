using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Ion.Engine.Constants;
using Ion.Engine.Misc;
using Ion.IR.Misc;
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
            {"#", TokenType.SymbolHash},
            {";", TokenType.SymbolSemiColon}
        }.SortByKeyLength();

        public static Dictionary<string, TokenType> instructions = new Dictionary<string, TokenType>
        {
            {InstructionName.Call, TokenType.InstructionCall},
            {InstructionName.End, TokenType.InstructionEnd},
            {InstructionName.Create, TokenType.InstructionCreate},
            {InstructionName.Set, TokenType.InstructionSet}
        }.SortByKeyLength<TokenType>();

        public static Dictionary<string, TokenType> simple = new[]
        {
            TokenConstants.instructions,
            TokenConstants.symbols
        }.SelectMany(dictionary => dictionary)
        .ToLookup(pair => pair.Key, pair => pair.Value)
        .ToDictionary(group => group.Key, group => group.First())
        .SortByKeyLength<TokenType>();

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
