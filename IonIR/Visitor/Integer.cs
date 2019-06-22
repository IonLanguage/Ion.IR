using Ion.Engine.Llvm;
using Ion.IR.Constructs;

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

        public LlvmValue AsLlvmValue()
        {
            // Convert to a constant and return as an llvm value wrapper instance.
            return LlvmConstFactory.Int(this.Kind.AsLlvmType(), this.Value);
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new System.NotImplementedException();
        }
    }
}
