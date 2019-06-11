using System;
using Ion.Engine.Misc;
using Ion.IR.Cognition;
using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class ValueParser : IParser<Value>
    {
        public Value Parse(ParserContext context)
        {
            // Invoke the kind parser.
            Kind kind = new KindParser().Parse(context);

            // Abstract current token.
            Token token = context.Stream.Current;

            // Abstract the token's value.
            string tokenValue = token.Value;

            // Ensure current token is recognized as a valid value.
            if (!Recognition.IsLiteral(token))
            {
                throw new Exception($"Invalid token provided as value: {token.Type}");
            }
            // Strip character/string delimiters if applicable.
            else if (token.Type == TokenType.LiteralString || token.Type == TokenType.LiteralCharacter)
            {
                tokenValue = Util.ExtractStringLiteralValue(tokenValue);
            }

            // Create the value construct.
            Value value = new Value(kind, tokenValue);

            // Return the construct.
            return value;
        }
    }
}
