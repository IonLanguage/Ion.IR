namespace Ion.IR.Visitor
{
    public class Function : Expr
    {
        public Prototype Prototype { get; }

        public Expr Body { get; }

        public Function(Prototype prototype, Expr body)
        {
            this.Prototype = prototype;
            this.Body = body;
        }
    }
}
