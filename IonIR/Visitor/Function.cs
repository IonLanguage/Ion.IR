using Ion.IR.Constructs;

namespace Ion.IR.Visitor
{
    public class Function : Construct
    {
        public override ConstructType ConstructType => ConstructType.Function;

        public Prototype Prototype { get; }

        public Construct Body { get; }

        public Function(Prototype prototype, Construct body)
        {
            this.Prototype = prototype;
            this.Body = body;
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new System.NotImplementedException();
        }
    }
}
