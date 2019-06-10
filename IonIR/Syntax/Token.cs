using Ion.Engine.Syntax;

namespace Ion.IR.Syntax
{
    public class Token : GenericToken<TokenType>
    {
        public Token(TokenType type, string value, int startPos) : base(type, value, startPos)
        {
            //
        }
    }
}
