using Ion.IR.Constructs;
using Ion.IR.Target;

namespace Ion.IR.Visitor
{
    public class IntegerExpr : Expr
    {
        public Kind Kind { get; }

        public long Value { get; }

        public IntegerExpr(Kind kind, long value)
        {
            this.Kind = kind;
            this.Value = value;
        }

        public LlvmValue AsLlvmValue()
        {
            // Convert to a constant and return as an llvm value wrapper instance.
            return LlvmConstFactory.Int(this.Kind.AsLlvmType(), this.Value);
        }
    }
}
