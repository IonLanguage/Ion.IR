using System;
using Ion.Engine.Constants;
using Ion.IR.Cognition;
using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class InputParser : IParser<IConstruct>
    {
        public IConstruct Parse(ParserContext context)
        {
            // Abstract the current token.
            Token token = context.Stream.Current;

            // Id construct.
            if (token.Type == TokenType.SymbolDollar)
            {
                return new ReferenceParser().Parse(context);
            }

            // Otherwise, it must be a value.
            return new ValueParser().Parse(context);
        }
    }
}
