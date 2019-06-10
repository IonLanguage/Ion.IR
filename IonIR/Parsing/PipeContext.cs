using Ion.IR.Misc;

namespace Ion.IR.Parsing
{
    public class PipeContext
    {
        public TokenStream Stream { get; }

        public PipeContext(TokenStream stream)
        {
            this.Stream = stream;
        }
    }
}
