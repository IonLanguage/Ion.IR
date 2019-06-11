using System;
using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class ArgParser : IParser<IConstruct>
    {
        public IConstruct Parse(ParserContext context)
        {
            // Abstract the token type of the current token.
            TokenType currentType = context.Stream.Current.Type;

            // Id construct.
            if (currentType == TokenType.SymbolDollar)
            {
                return new IdParser().Parse(context);
            }
            else
            {
                throw new Exception("Unrecognized construct");
            }
        }
    }
}
