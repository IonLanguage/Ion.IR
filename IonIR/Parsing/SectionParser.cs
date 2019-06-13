using System.Collections.Generic;
using Ion.IR.Constructs;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class SectionParser : IParser<Section>
    {
        public Section Parse(ParserContext context)
        {
            // Invoke identifier parser to capture section name.
            string name = new IdentifierParser().Parse(context);

            // Ensure current token is of type symbol colon.
            context.Stream.EnsureCurrent(TokenType.SymbolColon);

            // Skip symbol colon.
            context.Stream.Skip();

            // // Create the instruction buffer list.
            List<Instruction> instructions = new List<Instruction>();

            // Create the token buffer.
            Token buffer = context.Stream.Current;

            // Begin instruction parsing.
            while (buffer.Type != TokenType.SymbolTilde)
            {
                // Invoke instruction parser.
                Instruction instruction = new InstructionParser().Parse(context);

                // Append the instruction to the list.
                instructions.Add(instruction);
            }

            // Ensure current token is of type symbol tilde.
            context.Stream.EnsureCurrent(TokenType.SymbolTilde);

            // Skip symbol tilde.
            context.Stream.Skip();

            // Create the section construct.
            Section section = new Section(name, instructions.ToArray());

            // Return the construct.
            return section;
        }
    }
}
