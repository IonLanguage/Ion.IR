using Ion.Engine.Llvm;
using Ion.IR.Constants;
using Ion.IR.Constructs;
using Ion.IR.Handling;

namespace Ion.IR.Instructions
{
    public class StoreInst : Instruction
    {
        public LlvmValue Target { get; }

        public LlvmValue Value { get; }

        public StoreInst(LlvmValue target, LlvmValue value) : base(InstructionName.Set)
        {
            this.Target = target;
            this.Value = value;
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitStoreInst(this);
        }
    }
}
