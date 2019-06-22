using Ion.IR.Constants;
using Ion.IR.Constructs;

namespace Ion.IR.Instructions
{
    public class SetInst : Instruction
    {
        public Value Target { get; }

        public Value Value { get; }

        public SetInst(Value target, Value value) : base(InstructionName.Set, new IConstruct[]
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
