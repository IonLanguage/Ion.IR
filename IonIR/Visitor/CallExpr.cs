namespace Ion.IR.Visitor
{
    public class CallExpr : Expr
    {
        public string CalleeName { get; }

        public Expr[] Arguments { get; }

        public CallExpr(string calleeName, Expr[] arguments)
        {
            this.CalleeName = calleeName;
            this.Arguments = arguments;
        }

        public CallExpr(string calleeName) : this(calleeName, new Expr[] { })
        {
            //
        }
    }
}
