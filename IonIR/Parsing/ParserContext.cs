using Ion.IR.Misc;

namespace Ion.IR.Parsing
{
    public class ParserContext
    {
        public TokenStream Stream { get; }

        public ParserContext(TokenStream stream)
        {
            this.Stream = stream;
        }
    }
}
