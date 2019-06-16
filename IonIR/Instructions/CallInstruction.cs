using Ion.IR.Constants;
using Ion.IR.Constructs;

namespace Ion.IR.Instructions
{
    public class CallInstruction : Instruction
    {
        public string TargetIdentifier { get; }

        public string ResultIdentifier { get; }

        public Value[] Arguments { get; }

        public CallInstruction(string targetIdentifier, string resultIdentifier, Value[] arguments) : base(InstructionName.Call, arguments)
        {
            this.TargetIdentifier = targetIdentifier;
            this.ResultIdentifier = resultIdentifier;
            this.Arguments = arguments;
        }
    }
}
