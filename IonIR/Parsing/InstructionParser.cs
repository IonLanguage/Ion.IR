using System;
using System.Collections.Generic;
using Ion.IR.Constants;
using Ion.IR.Constructs;
using Ion.IR.Instructions;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public interface IInstructionParser<T> : IParser<T> where T : Instruction
    {
        //
    }

    public class InstructionParser : IInstructionParser<Instruction>
    {
        public Instruction Parse(ParserContext context)
        {
            context.Stream.EnsureCurrent(TokenType.SymbolHash);

            context.Stream.Skip();

            string resultIdentifier = new IdentifierParser().Parse(context);

            context.Stream.EnsureCurrent(TokenType.SymbolEqual);

            context.Stream.Skip();

            string name = new IdentifierParser().Parse(context);

            // Create a buffer for the current token.
            Token token = context.Stream.Get();

            // Create the inputs buffer list.
            List<IConstruct> inputs = new List<IConstruct>();

            // Instruction contains arguments.
            while (token.Type != TokenType.SymbolSemiColon)
            {
                // Invoke the input parser.
                IConstruct input = new InputParser().Parse(context);

                // Append the input to the list.
                inputs.Add(input);

                token = context.Stream.Get();
            }

            // Ensure current token is of type semi-colon.
            context.Stream.EnsureCurrent(TokenType.SymbolSemiColon);

            // Skip semi-colon token.
            context.Stream.Skip();

            Instruction inst;

            switch (name)
            {
                case InstructionName.End:
                    {
                        if (inputs.Count > 1)
                        {
                            throw new Exception("Unexpected amount of inputs");
                        }
                        else if (!(inputs[0] is Value))
                        {
                            throw new Exception("Expected input to be a value");
                        }

                        inst = new EndInst(inputs[0] as Value);

                        break;
                    }

                default:
                    {
                        throw new Exception($"Unrecognized instruction name: {name}");
                    }
            }

            return inst;
        }
    }
}
