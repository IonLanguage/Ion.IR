using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Ion.Engine.Constants;
using Ion.Engine.Misc;
using Ion.IR.Constructs;
using Ion.IR.Misc;
using Ion.IR.Syntax;
using LLVMSharp;

namespace Ion.IR.Constants
{
    public static class TokenConstants
    {
        public delegate LLVMTypeRef LlvmTypeGenerator();

        public static Dictionary<string, KindType> kindTypeMap = new Dictionary<string, KindType>
        {
            {KindName.Void, KindType.Void},
            {KindName.Boolean, KindType.Boolean},
            {KindName.Integer8, KindType.Integer8},
            {KindName.Integer16, KindType.Integer16},
            {KindName.Integer32, KindType.Integer32},
            {KindName.Integer64, KindType.Integer64},
            {KindName.Integer128, KindType.Integer128}

            // TODO: Missing float(s) and double(s).
        };

        public static Dictionary<KindType, string> kindReverseTypeMap = TokenConstants.kindTypeMap
            .ToDictionary((item) => item.Value, (item) => item.Key);

        public static Dictionary<KindType, LlvmTypeGenerator> kindGenerationMap = new Dictionary<KindType, LlvmTypeGenerator>
        {
            {KindType.Void, LLVM.VoidType},
            {KindType.Boolean, LLVM.Int1Type},
            {KindType.Integer8, LLVM.Int8Type},
            {KindType.Integer16, LLVM.Int16Type},
            {KindType.Integer32, LLVM.Int32Type},
            {KindType.Integer64, LLVM.Int64Type},
            {KindType.Integer128, LLVM.Int128Type}
        };

        public static Dictionary<string, TokenType> symbols = new Dictionary<string, TokenType>
        {
            {"@", TokenType.SymbolAt},
            {":", TokenType.SymbolColon},
            {"$", TokenType.SymbolDollar},
            {"#", TokenType.SymbolHash},
            {";", TokenType.SymbolSemiColon},
            {"(", TokenType.SymbolParenthesesL},
            {")", TokenType.SymbolParenthesesR},
            {"[", TokenType.SymbolBracketL},
            {"]", TokenType.SymbolBracketR},
            {",", TokenType.SymbolComma},
            {"~", TokenType.SymbolTilde}
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
