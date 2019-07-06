using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class VarDeclare : Construct
    {
        public override ConstructType ConstructType => ConstructType.VarDeclare;

        public Kind Kind { get; }

        public Value Value { get; }

        public VarDeclare(Kind kind, Value value)
        {
            this.Kind = kind;
            this.Value = value;
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitVarDeclare(this);
        }

        public override string ToString()
        {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}