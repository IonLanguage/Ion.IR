using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class NumericExpr : Construct
    {
        public override ConstructType ConstructType => ConstructType.NumericExpr;

        public override Construct Accept(LlvmVisitor visitor)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}