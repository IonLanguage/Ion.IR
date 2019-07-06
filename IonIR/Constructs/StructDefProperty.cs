using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class StructDefProperty : Construct
    {
        public override ConstructType ConstructType => throw new System.NotImplementedException();

        public Kind Kind { get; }

        public string Identifier { get; }

        public StructDefProperty(Kind kind, string identifier)
        {
            this.Kind = kind;
            this.Identifier = identifier;
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitStructDefProperty(this);
        }
    }
}