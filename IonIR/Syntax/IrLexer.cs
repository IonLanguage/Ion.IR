#nullable enable

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ion.Engine.Syntax;
using Ion.IR.Constants;
using Ion.Engine.Misc;
using Ion.Engine.Constants;

namespace Ion.IR.Syntax
{
    public class IrLexer : Lexer<Token, TokenType>
    {
        protected readonly bool ignoreWhitespace;

        public IrLexer(string input, bool ignoreWhitespace = true) : base(input)
        {
            this.ignoreWhitespace = ignoreWhitespace;
        }

        public override Token[] Tokenize()
        {
            // Ensure input contains more than zero characters.
            if (this.Input.Length <= 0)
            {
                throw new Exception("Input must contain at least one character");
            }

            // Set initial position.
            this.SetPosition(0);

            // Create the resulting list.
            List<Token> tokens = new List<Token>();

            // Create the next token buffer.
            Token? nextToken = this.GetNextToken();

            // Obtain all possible tokens.
            while (nextToken != null)
            {
                // If the token is unknown, issue a warning in console.
                if (nextToken.Type == TokenType.Unknown)
                {
                    // TODO: This should be done through ErrorReporting (implement in the future).
                    Console.WriteLine($"Warning: Unexpected token type to be unknown, value: {nextToken.Value}");
                }

                // Append token value to the result list.
                tokens.Add(nextToken);

                // Continue enumeration.
                nextToken = this.GetNextToken();
            }

            // Return the resulting tokens as an array.
            return tokens.ToArray();
        }

        protected Token? GetNextToken()
        {
            // Return immediatly if position overflows.
            if (this.Position >= this.Input.Length)
            {
                return null;
            }
            else if (!this.Char.HasValue)
            {
                throw new Exception("Expected current character to not be null");
            }

            // Begin capturing the token. Identify the token as unknown initially.
            Token token = new Token(TokenType.Unknown, this.Char.Value.ToString(), this.Position);

            // Whitespace.
            if (char.IsWhiteSpace(this.Char.Value))
            {
                // Ignore whitespace flag is set, skip.
                if (this.ignoreWhitespace)
                {
                    while (char.IsWhiteSpace(this.Char.Value))
                    {
                        this.Skip();
                    }
                }
                // Match all whitespace characters until we hit a normal character.
                else if (this.MatchExpression(token, TokenType.Whitespace, Pattern.ContinuousWhitespace, out token))
                {
                    // Return the token.
                    return token;
                }
            }

            // Test string against simple token type values.
            foreach (var pair in TokenConstants.simple)
            {
                // Possible candidate.
                if (pair.Key.StartsWith(this.Char.Value))
                {
                    // Create initial regex.
                    Regex pattern = Util.CreateRegex(Regex.Escape(pair.Key));

                    // If the match starts with a letter, modify the regex to force either whitespace or EOF at the end.
                    if (Pattern.Identifier.IsMatch(pair.Key))
                    {
                        // Modify the regex to include whitespace/EOF/semi-colon at the end.
                        pattern = Util.CreateRegex($@"{Regex.Escape(pair.Key)}([^a-zA-Z_0-9])");
                    }

                    // If the symbol is next in the input.
                    if (this.MatchExpression(token, pair.Value, pattern, out token))
                    {
                        // Reduce the position.
                        // TODO: Causing problems, works when commented HERE.
                        //this.SetPosition(this.Position - token.Value.Length - pair.Key.Length);

                        // Skim the last character off.
                        token = new Token(token.Type, pair.Key, token.StartPos);

                        // Return the token.
                        return token;
                    }
                }
            }

            // Complex types support.
            foreach (var pair in TokenConstants.complex)
            {
                // If it matches, return the token (already modified by the function).
                if (this.MatchExpression(token, pair.Value, pair.Key, out token))
                {
                    // Return the token.
                    return token;
                }
            }

            // At this point the token was not identified. Skip over any captured value.
            this.Skip(token.Value != null ? token.Value.Length : 0);

            // Return the default token. The token type defaults to unknown.
            return token;
        }

        /// <summary>
        /// Checks for a positive match for a complex type or just generic regex,
        /// if positive, it'll update the referenced token to the provided type with
        /// the matched text.
        /// </summary>
        protected bool MatchExpression(Token token, TokenType type, Regex regex, out Token result)
        {
            // Substrings from the current position to get the viable matching string.
            string input = this.Input.Substring(this.Position);
            Match match = regex.Match(input);

            // If successful, return a new token with different value and type.
            if (match.Success && match.Index == 0)
            {
                // Modify the result.
                result = new Token(type, match.Value, token.StartPos);

                // Skip the capture value's amount.
                this.Skip(result.Value.Length);

                // Return true to indicate success.
                return true;
            }

            // Replace result with the original token, making no changes.
            result = token;

            // Return false to indicate failure.
            return false;
        }
    }
}
