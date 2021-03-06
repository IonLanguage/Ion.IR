using System;
using System.Collections.Generic;
using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class RoutineParser : IParser<Routine>
    {
        public Routine Parse(ParserContext context)
        {
            // Ensure current token is of type symbol at.
            context.Stream.EnsureCurrent(TokenType.SymbolAt);

            // Skip symbol at token.
            context.Stream.Skip();

            // Invoke the identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Ensure current token is of type parentheses start.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesL);

            // Skip parentheses start token.
            context.Stream.Skip();

            // Capture the current token as the buffer.
            Token buffer = context.Stream.Current;

            // Create the argument buffer list.
            List<(Kind, Reference)> args = new List<(Kind, Reference)>();

            // Begin argument parsing.
            while (buffer.Type != TokenType.SymbolParenthesesR)
            {
                // Invoke kind parser.
                Kind kind = new KindParser().Parse(context);

                // Invoke reference parser.
                Reference reference = new ReferenceParser().Parse(context);

                // Abstract current token.
                Token token = context.Stream.Current;

                // Current token must be symbol comma or parentheses end.
                if (token.Type != TokenType.SymbolParenthesesR && token.Type != TokenType.SymbolComma)
                {
                    throw new Exception($"Unexpected token in argument list: {token.Type}");
                }
                // Skip symbol comma token.
                else if (token.Type == TokenType.SymbolComma)
                {
                    context.Stream.Skip();
                }

                // Append the argument.
                args.Add((kind, reference));

                // Update the buffer.
                buffer = context.Stream.Current;
            }

            // Ensure current token is symbol parentheses end.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesR);

            // Skip parentheses end.
            context.Stream.Skip();

            // Invoke kind parser to parse return kind.
            Kind returnKind = new KindParser().Parse(context);

            // Update the token buffer.
            buffer = context.Stream.Current;

            // Parse the body.
            Section body = new SectionParser().Parse(context);

            // Ensure current token is of type symbol tilde.
            context.Stream.EnsureCurrent(TokenType.SymbolTilde);

            // Skip symbol tilde.
            context.Stream.Skip();

            // Create the prototype.
            Prototype prototype = new Prototype(identifier, args.ToArray(), returnKind, false);

            // Create the routine construct.
            Routine routine = new Routine(prototype, body);

            // Return the resulting routine.
            return routine;
        }
    }
}
