using System.Collections.Generic;
using Ion.Engine.CodeGeneration.Helpers;
using Ion.Engine.Parsing;
using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class InstructionParser : IParser<Instruction>
    {
        public Instruction Parse(ParserContext context)
        {
            // Ensure current token is of type identifier.
            context.Stream.EnsureCurrent(TokenType.Identifier);

            // Capture instruction name.
            string name = context.Stream.Current.Value;

            // Skip identifier token.
            context.Stream.Skip();

            // Create a buffer for the current token.
            Token token = context.Stream.Get();

            // Create the argument buffer list.
            List<IConstruct> args = new List<IConstruct>();

            // Instruction contains arguments.
            while (token.Type != TokenType.SymbolSemiColon)
            {
                // Invoke the argument parser.
                IConstruct arg = new ArgParser().Parse(context);

                // Append the argument to the list.
                args.Add(arg);
            }

            // Ensure current token is of type semi-colon.
            context.Stream.EnsureCurrent(TokenType.SymbolSemiColon);

            // Skip semi-colon token.
            context.Stream.Skip();

            // Create the instruction construct.
            Instruction instruction = new Instruction(name, args.ToArray());

            // Return the resulting instruction.
            return instruction;
        }
    }
}
