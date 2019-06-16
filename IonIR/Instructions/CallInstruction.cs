using Ion.IR.Constants;
using Ion.IR.Constructs;

namespace Ion.IR.Instructions
{
    public class CallInstruction : Instruction
    {
        public Value[] Arguments { get; }

        public CallInstruction(Value[] arguments) : base(InstructionName.Call, arguments)
        {
            this.Arguments = arguments;
        }
    }
}
