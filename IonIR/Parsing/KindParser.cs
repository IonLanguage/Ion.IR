using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class KindParser : IParser<Kind>
    {
        public Kind Parse(ParserContext context)
        {
            // Ensure current type is symbol parentheses start.
            context.Stream.EnsureCurrent(TokenType.SymbolColon);

            // Skip parentheses start.
            context.Stream.Skip();

            // Invoke identifier parser.
            string name = new IdentifierParser().Parse(context);

            // TODO: Pointer support.

            // Create the kind construct.
            Kind kind = new Kind(name);

            // Return the construct.
            return kind;
        }
    }
}
