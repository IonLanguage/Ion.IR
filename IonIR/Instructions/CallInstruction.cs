using Ion.IR.Constants;
using Ion.IR.Constructs;
using Ion.IR.Target;

namespace Ion.IR.Instructions
{
    public class CallInstruction : Instruction
    {
        public LlvmFunction Target { get; }

        public string ResultIdentifier { get; }

        public Value[] Arguments { get; }

        public CallInstruction(LlvmFunction target, string resultIdentifier, Value[] arguments) : base(InstructionName.Call, arguments)
        {
            this.Target = target;
            this.ResultIdentifier = resultIdentifier;
            this.Arguments = arguments;
        }

        public LlvmValue AsLlvmValue()
        {
            // Create the builder reference.
            LlvmBuilder builder = LlvmBuilder.CreateReference();

            // Create and return the call instruction.
            return builder.CreateCall(this.Target, this.ResultIdentifier, this.Arguments.AsLlvmValues());
        }
    }
}
