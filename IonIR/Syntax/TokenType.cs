namespace Ion.IR.Syntax
{
    public enum TokenType
    {
        Unknown,

        Whitespace,

        Comment,

        Identifier,

        LiteralString,

        LiteralCharacter,

        LiteralDecimal,

        LiteralNumber,

        SymbolAt,

        SymbolColon,

        SymbolComma,

        SymbolDollar,

        SymbolHash,

        SymbolSemiColon,

        SymbolParenthesesL,

        SymbolParenthesesR,

        SymbolBracketL,

        SymbolBracketR,

        InstructionCall,

        InstructionEnd,

        InstructionCreate,

        InstructionSet
    }
}
