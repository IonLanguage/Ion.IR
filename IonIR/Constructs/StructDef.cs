using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class StructDef : Construct
    {
        public override ConstructType ConstructType => ConstructType.StructDef;

        public string Identifier { get; }

        public StructDefProperty[] Body { get; }

        public StructDef(string identifier, StructDefProperty[] body)
        {
            this.Identifier = identifier;
            this.Body = body;
        }

        public override string ToString()
        {
            // TODO
            throw new System.NotImplementedException();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitStructDef(this);
        }
    }
}