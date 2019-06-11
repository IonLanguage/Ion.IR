using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class IdentifierParser : IParser<string>
    {
        public string Parse(ParserContext context)
        {
            // Ensure current token is of type identifier.
            context.Stream.EnsureCurrent(TokenType.Identifier);

            // Capture the identifier.
            string identifier = context.Stream.Current.Value;

            // Skip the identifier token.
            context.Stream.Skip();

            // Return the identifier.
            return identifier;
        }
    }
}
