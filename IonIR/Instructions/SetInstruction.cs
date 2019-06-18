using Ion.IR.Constants;
using Ion.IR.Constructs;

namespace Ion.IR.Instructions
{
    public class SetInstruction : Instruction
    {
        public Value Target { get; }

        public Value Value { get; }

        public SetInstruction(Value target, Value value) : base(InstructionName.Set, new IConstruct[]
        {
            target,
            value
        })
        {
            this.Target = target;
            this.Value = value;
        }
    }
}
