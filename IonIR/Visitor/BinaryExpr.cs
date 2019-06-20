using System;

namespace Ion.IR.Visitor
{
    public enum BinaryExprType
    {
        Addition,

        Substraction,

        Division,

        Multiplication,

        LessThan
    }

    public class BinaryExpr : Expr, IExprVisitable
    {
        public Expr LeftSide { get; }

        public Expr RightSide { get; }

        public BinaryExprType Type { get; }

        public override ExprType ExprType => ExprType.Binary;

        public BinaryExpr(char operation, Expr leftSide, Expr rightSide)
        {
            // Attempt to identify operation and mark as own expression type.
            switch (operation)
            {
                case '+':
                    {
                        this.Type = BinaryExprType.Addition;

                        break;
                    }

                case '-':
                    {
                        this.Type = BinaryExprType.Substraction;

                        break;
                    }

                case '*':
                    {
                        this.Type = BinaryExprType.Multiplication;

                        break;
                    }

                case '/':
                    {
                        this.Type = BinaryExprType.Division;

                        break;
                    }

                case '<':
                    {
                        this.Type = BinaryExprType.LessThan;

                        break;
                    }

                default:
                    {
                        throw new Exception($"Invalid operation character: {operation}");
                    }
            }

            this.LeftSide = leftSide;
            this.RightSide = rightSide;
        }

        public override Expr Accept(ExprVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
