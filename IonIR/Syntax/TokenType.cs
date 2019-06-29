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

        SymbolEqual,

        SymbolSemiColon,

        SymbolParenthesesL,

        SymbolParenthesesR,

        SymbolBracketL,

        SymbolBracketR,

        SymbolExclamation,

        SymbolPercent,

        InstructionCall,

        InstructionEnd,

        InstructionCreate,

        InstructionSet
    }
}
