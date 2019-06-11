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

        SymbolDollar,

        SymbolHash,

        SymbolSemiColon,

        SymbolParenthesesL,

        SymbolParenthesesR,

        InstructionCall,

        InstructionEnd,

        InstructionCreate,

        InstructionSet
    }
}
