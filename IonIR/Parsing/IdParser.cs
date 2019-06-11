using Ion.Engine.CodeGeneration.Helpers;
using Ion.Engine.Parsing;
using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class IdParser : IParser<Id>
    {
        public Id Parse(ParserContext context)
        {
            // Ensure current token is of type symbol dollar.
            context.Stream.EnsureCurrent(TokenType.SymbolDollar);

            // Skip symbol dollar token.
            context.Stream.Skip();

            // Ensure current token is of type identifier.
            context.Stream.EnsureCurrent(TokenType.Identifier);

            // Capture the identifier's value.
            string value = context.Stream.Current.Value;

            // Skip the identifier token.
            context.Stream.Skip();

            // Create the id construct.
            Id id = new Id(value);

            // Return the resulting id construct.
            return id;
        }
    }
}
