namespace Ion.IR.Visitor
{
    public abstract class Expr
    {
        public abstract ExprType ExprType { get; }

        public virtual Expr VisitChildren(ExprVisitor visitor)
        {
            return visitor.Visit(this);
        }

        public virtual Expr Accept(ExprVisitor visitor)
        {
            return visitor.VisitExtension(this);
        }
    }
}
