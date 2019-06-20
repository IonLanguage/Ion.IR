#nullable enable

using System;
using System.Collections.Generic;
using Ion.Engine.Misc;
using Ion.IR.Target;
using LLVMSharp;

namespace Ion.IR.Visitor
{
    public abstract class ExprVisitor
    {
        protected readonly Stack<LlvmValue> valueStack = new Stack<LlvmValue>();

        protected readonly Dictionary<string, LlvmValue> namedValues = new Dictionary<string, LlvmValue>();

        protected readonly LlvmModule module;

        protected readonly LlvmBuilder builder;

        public ExprVisitor(LlvmModule module, LlvmBuilder builder)
        {
            this.module = module;
            this.builder = builder;
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

            // Create the call.
            LlvmValue call = this.builder.CreateCall(callee, SpecialName.Temporary, arguments.ToArray());

            // Push the call onto the stack.
            this.valueStack.Push(call);

            // Return the node.
            return node;
        }

        public Expr VisitPrototype(Prototype node)
        {
            // Make the function type:  double(double,double) etc.
            var argumentCount = (uint)node.Arguments.Count;
            var arguments = new LLVMTypeRef[Math.Max(argumentCount, 1)];

            // Attempt to retrieve an existing function value.
            LlvmFunction? function = this.module.GetFunction(node.Identifier);

            // Function may be already defined.
            if (function != null)
            {
                // Function already has a body, disallow re-definition.
                if (function.HasBlocks)
                {
                    throw new Exception($"Cannot re-define function: {node.Identifier}");
                }
                // If the function takes a different number of arguments, reject.
                else if (function.ArgumentCount != argumentCount)
                {
                    throw new Exception("redefinition of function with different # args");
                }
            }
            else
            {
                for (int i = 0; i < argumentCount; ++i)
                {
                    arguments[i] = LLVM.DoubleType();
                }

                // TODO: Support for infinite arguments and hard-coded return type.
                // Create the function within the module.
                function = this.module.CreateFunction(node.Identifier, LLVM.FunctionType(LLVM.VoidType(), arguments, false).Wrap());

                // Set the function's linkage.
                function.SetLinkage(LLVMLinkage.LLVMExternalLinkage);
            }

            // Process arguments.
            for (int i = 0; i < argumentCount; ++i)
            {
                string argumentName = node.Arguments[i];

                // Retrieve the argument at the current index iterator.
                LlvmValue argument = function.GetArgumentAt((uint)i);

                // Name the argument.
                argument.SetName(argumentName);

                // TODO: Watch out for already existing ones.
                // Stored the named argument in the named values cache.
                this.namedValues.Add(argumentName, argument);
            }

            // Push the function onto the stack.
            this.valueStack.Push(function);

            // Return the node.
            return node;
        }

        public Expr VisitFunction(Function node)
        {
            // Clear named values to reset scope upon processing a new function.
            this.namedValues.Clear();

            // Visit the function's prototype.
            this.Visit(node.Prototype);

            // Pop the function prototype from the value stack.
            LlvmFunction function = (LlvmFunction)this.valueStack.Pop();

            // Create the entry block for the function, and retrieve its corresponding builder.
            LlvmBuilder builder = function.AppendBlock(SpecialName.Entry).Builder;

            // Attempt to visit the body.
            try
            {
                this.Visit(node.Body);
            }
            // Delete the function for error recovery.
            catch (Exception)
            {
                function.Delete();

                throw;
            }

            // Create the return instruction.
            builder.CreateReturn(this.valueStack.Pop());

            // Validate the function.
            function.Verify();

            // Push the function onto the stack.
            this.valueStack.Push(function);

            // Return the node.
            return node;
        }
    }
}
