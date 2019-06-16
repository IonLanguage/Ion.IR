using Ion.IR.Constants;
using Ion.IR.Constructs;

namespace Ion.IR.Instructions
{
    public class EndInstruction : Instruction
    {
        public Value Value { get; }

        public EndInstruction(Value value) : base(InstructionName.End, new IConstruct[]
        {
            value
        })
        {
            this.Value = value;
        }
    }
}
