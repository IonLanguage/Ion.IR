using Ion.Engine.Llvm;
using Ion.IR.Constructs;
using Ion.IR.Handling;

namespace Ion.IR.Visitor
{
    public class Integer : Construct
    {
        public override ConstructType ConstructType => ConstructType.Integer;

        public Kind Kind { get; }

        public long Value { get; }

        public Integer(Kind kind, long value)
        {
            this.Kind = kind;
            this.Value = value;
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new System.NotImplementedException();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitInteger(this);
        }
    }
}
