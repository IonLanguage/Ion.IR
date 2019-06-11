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

        SymbolTilde,

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
