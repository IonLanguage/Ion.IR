using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class MetadataParser : IParser<Metadata>
    {
        public Metadata Parse(ParserContext context)
        {
            // Ensure current token type to be of symbol exclamation.
            context.Stream.EnsureCurrent(TokenType.SymbolExclamation);

            // Skip symbol exclamation token.
            context.Stream.Skip();

            // TODO: Key and value are delimitered-string literals, not identifiers.

            // Parse key.
            string key = new IdentifierParser().Parse(context);

            // Parse the value.
            string value = new IdentifierParser().Parse(context);

            // Create the construct.
            Metadata metadata = new Metadata(key, value);

            // Return the construct.
            return metadata;
        }
    }
}
