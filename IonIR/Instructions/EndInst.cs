using Ion.IR.Constants;
using Ion.IR.Constructs;
using Ion.IR.Handling;

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

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitEndInst(this);
        }
    }
}
