using System;
using Ion.IR.Constructs;
using Ion.IR.Handling;

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

    public class BinaryExpr : Construct
    {
        public override ConstructType ConstructType => ConstructType.BinaryExpr;

        public string ResultIdentifier { get; }

        public Construct LeftSide { get; }

        public Construct RightSide { get; }

        public BinaryExprType Type { get; }

        public BinaryExpr(char operation, Construct leftSide, Construct rightSide, string resultIdentifier)
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

            this.ResultIdentifier = resultIdentifier;
            this.LeftSide = leftSide;
            this.RightSide = rightSide;
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitBinaryExpr(this);
        }
    }
}
