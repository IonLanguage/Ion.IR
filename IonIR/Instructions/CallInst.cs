using Ion.Engine.Llvm;
using Ion.IR.Constants;
using Ion.IR.Constructs;

namespace Ion.IR.Instructions
{
    public class CallInst : Instruction
    {
        public LlvmFunction Callee { get; }

        public string ResultIdentifier { get; }

        public Value[] Arguments => (Value[])this.GetFrom(1);

        // TODO: What about ResultIdentifier?
        public CallInst(LlvmFunction target, string resultIdentifier, IConstruct[] inputs) : base(InstructionName.Call, inputs)
        {
            this.Callee = target;
            this.ResultIdentifier = resultIdentifier;
        }
    }
}
