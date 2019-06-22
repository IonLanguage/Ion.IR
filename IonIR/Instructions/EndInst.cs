using Ion.IR.Constants;
using Ion.IR.Constructs;

namespace Ion.IR.Instructions
{
    public class EndInst : Instruction
    {
        public Value Value { get; }

        public EndInst(Value value) : base(InstructionName.End, new IConstruct[]
        {
            value
        })
        {
            this.Value = value;
        }
    }
}
