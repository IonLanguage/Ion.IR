using Ion.IR.Constants;
using Ion.IR.Constructs;

namespace Ion.IR.Instructions
{
    public class SetInstruction : Instruction
    {
        public Reference Reference { get; }

        public Value Value { get; }

        public SetInstruction(Reference reference, Value value) : base(InstructionName.Set, new IConstruct[]
        {
            reference,
            value
        })
        {
            this.Reference = reference;
            this.Value = value;
        }
    }
}
