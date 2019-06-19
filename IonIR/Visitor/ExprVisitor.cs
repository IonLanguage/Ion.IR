#nullable enable

using System;
using System.Collections.Generic;
using Ion.IR.Target;

namespace Ion.IR.Visitor
{
    public abstract class ExprVisitor
    {
        protected readonly Stack<LlvmValue> valueStack = new Stack<LlvmValue>();

        protected readonly Dictionary<string, LlvmValue> namedValues = new Dictionary<string, LlvmValue>();

        protected readonly LlvmModule module;

        public ExprVisitor(LlvmModule module)
        {
            this.module = module;
        }

        public Expr Visit(Expr node)
        {
            if (node != null)
            {
                return node.Accept(this);
            }

            return null;
        }

        public Expr VisitExtension(Expr node)
        {
            return node.VisitChildren(this);
        }

        public Expr VisitIntegerExpr(IntegerExpr node)
        {
            // Push the node's llvm value to the stack.
            this.valueStack.Push(node.AsLlvmValue());

            // Return the node.
            return node;
        }

        public Expr VisitVariableExpr(VariableExpr node)
        {
            // Create the value buffer.
            LlvmValue value;

            // Attempt to lookup the value by its name.
            if (this.namedValues.TryGetValue(node.Name, out value))
            {
                // Append the value to the stack.
                this.valueStack.Push(value);
            }
            // At this point, reference variable is non-existent.
            else
            {
                throw new Exception($"Undefined reference to variable: '{node.Name}'");
            }

            // Return the node.
            return node;
        }

        public Expr VisitCallExpr(CallExpr node)
        {
            // Attempt to retrieve the function.
            LlvmFunction? callee = this.module.GetFunction(node.CalleeName);

            // Ensure callee function is not null.
            if (callee == null)
            {
                throw new Exception($"Call to undefined function: {node.CalleeName}");
            }

            // Create the argument buffer list, which will be populated from the stack.
            List<LlvmValue> arguments = new List<LlvmValue>();

            // Visit call arguments.
            foreach (Expr argument in node.Arguments)
            {
                this.Visit(argument);

                // Pop value from the stack and append to the buffer list.
                arguments.Add(this.valueStack.Pop());
            }

            // Push the call onto the stack.
            this.valueStack.Push()
        }
    }
}
