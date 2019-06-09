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

        InstructionCall,

        InstructionEnd,

        InstructionCreate,

        InstructionSet
    }
}
