using System;
using Ion.Engine.CodeGeneration.Helpers;
using Ion.Engine.Parsing;
using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class RoutineParser : IParser<Routine>
    {
        public Routine Parse(ParserContext context)
        {
            // Ensure current token is of type symbol at.
            context.Stream.EnsureCurrent(TokenType.SymbolAt);

            // Skip symbol at token.
            context.Stream.Skip();

            // Invoke the identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Ensure current token is of type parentheses start.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesL);

            // Skip parentheses start token.
            context.Stream.Skip();

            // Capture the current token as the buffer.
            Token buffer = context.Stream.Current;

            // Begin argument parsing.
            while (buffer.Type != TokenType.SymbolParenthesesR)
            {
                // TODO: Finish implementing.

                buffer = context.Stream.Current;
            }

            throw new NotImplementedException();
        }
    }
}
