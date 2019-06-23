using Ion.IR.Constructs;
using Ion.IR.Handling;

namespace Ion.IR.Visitor
{
    public class Function : Construct
    {
        public override ConstructType ConstructType => ConstructType.Function;

        public Prototype Prototype { get; }

        public Section Body { get; }

        public Function(Prototype prototype, Section body)
        {
            this.Prototype = prototype;
            this.Body = body;
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new System.NotImplementedException();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitFunction(this);
        }
    }
}
