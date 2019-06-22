using Ion.IR.Constructs;

namespace Ion.IR.Visitor
{
    public class CallExpr : Construct
    {
        public override ConstructType ConstructType => ConstructType.Call;

        public string CalleeName { get; }

        public Construct[] Arguments { get; }

        public CallExpr(string calleeName, Construct[] arguments)
        {
            this.CalleeName = calleeName;
            this.Arguments = arguments;
        }

        public CallExpr(string calleeName) : this(calleeName, new Construct[] { })
        {
            //
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new System.NotImplementedException();
        }
    }
}
