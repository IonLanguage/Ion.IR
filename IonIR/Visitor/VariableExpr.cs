namespace Ion.IR.Visitor
{
    public class VariableExpr : Expr
    {
        public string Name { get; }

        public VariableExpr(string name)
        {
            this.Name = name;
        }
    }
}
