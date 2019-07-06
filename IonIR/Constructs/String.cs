using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class String : Construct
    {
        public override ConstructType ConstructType => ConstructType.String;

        public string Value { get; }

        public String(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new System.NotImplementedException();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitString(this);
        }
    }
}