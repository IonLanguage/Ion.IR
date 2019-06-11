using Ion.Engine.CodeGeneration.Helpers;
using Ion.Engine.Parsing;
using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class ReferenceParser : IParser<Reference>
    {
        public Reference Parse(ParserContext context)
        {
            // Ensure current token is of type symbol dollar.
            context.Stream.EnsureCurrent(TokenType.SymbolDollar);

            // Skip symbol dollar token.
            context.Stream.Skip();

            // Invoke the identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Create the reference construct.
            Reference reference = new Reference(identifier);

            // Return the resulting id construct.
            return reference;
        }
    }
}
