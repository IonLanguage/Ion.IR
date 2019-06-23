using Ion.Engine.Llvm;
using Ion.IR.Constants;
using Ion.IR.Constructs;
using Ion.IR.Handling;

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

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitCallInst(this);
        }
    }
}
