using Ion.IR.Constants;
using Ion.IR.Constructs;

namespace Ion.IR.Instructions
{
    public class CreateInstruction : Instruction
    {
        public string Identifier { get; }

        public Kind Kind { get; }

        public CreateInstruction(string identifier, Kind kind) : base(InstructionName.Create, new IConstruct[]
        {
            new Reference(identifier),
            kind
        })
        {
            this.Identifier = identifier;
            this.Kind = kind;
        }
    }
}
