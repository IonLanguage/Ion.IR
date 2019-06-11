using System;
using Ion.Engine.Syntax;
using Ion.IR.Syntax;

namespace Ion.IR.Misc
{
    public class TokenStream : Stream<Token>
    {
        public TokenStream(Token[] items) : base(items)
        {
            //
        }

        /// <summary>
        /// Ensure that the current token's type
        /// matches the token type provided, otherwise
        /// throw an error.
        /// </summary>
        public void EnsureCurrent(TokenType type)
        {
            // Capture the current token.
            Token token = this.Get();

            // Ensure current token's type matches provided token type.
            if (token.Type != type)
            {
                throw new Exception($"Expected current token to be of type '{type}' but got '{token.Type}'");
            }
        }
    }
}
