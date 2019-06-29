using System;
using System.Collections.Generic;
using Ion.Engine.Llvm;
using Ion.IR.Constructs;
using Ion.IR.Instructions;

namespace Ion.IR.Handling
{
    public partial class LlvmVisitor
    {
        public Construct VisitCallInst(CallInst node)
        {
            // Create an argument buffer list.
            List<LlvmValue> arguments = new List<LlvmValue>();

            // Emit the call arguments.
            foreach (Constructs.Construct argument in node.Arguments)
            {
                // Continue if the argument is null.
                if (argument == null)
                {
                    continue;
                }

                // Visit the argument.
                this.Visit(argument);

                // Pop the argument off the value stack.
                LlvmValue argumentValue = this.valueStack.Pop();

                // Append argument value to the argument buffer list.
                arguments.Add(argumentValue);
            }

            // Retrieve the callee function.
            LlvmFunction callee = node.Callee;

            // Ensure argument count is correct (with continuous arguments).
            if (callee.HasInfiniteArguments && arguments.Count < callee.ArgumentCount - 1)
            {
                throw new Exception($"Target function requires at least {callee.ArgumentCount - 1} argument(s)");
            }
            // Otherwise, expect the argument count to be exact.
            else if (arguments.Count != callee.ArgumentCount)
            {
                throw new Exception($"Argument amount mismatch, target function requires exactly {callee.ArgumentCount} argument(s)");
            }

            // Create the function call.
            LlvmValue call = this.builder.CreateCall(callee, node.ResultIdentifier, arguments.ToArray());

            // Append the value onto the stack.
            this.valueStack.Push(call);

            // Return the node.
            return node;
        }

        public Construct VisitStoreInst(StoreInst node)
        {
            // Create the LLVM store instruction.
            LlvmValue value = this.builder.CreateStore(node.Value, node.Target);

            // Append the resulting instruction onto the stack.
            this.valueStack.Push(value);

            // Return the node.
            return node;
        }

        public Construct VisitEndInst(EndInst node)
        {
            // Visit the value.
            this.VisitValue(node.Value);

            // Pop off the resulting value.
            LlvmValue value = this.valueStack.Pop();

            // Create the return instruction.
            LlvmValue returnInst = this.builder.CreateReturn(value);

            // Append the return instruction onto the stack.
            this.valueStack.Push(returnInst);

            // Return the node.
            return node;
        }

        public Construct VisitCreateInst(CreateInst node)
        {
            // Visit the kind.
            this.VisitKind(node.Kind);

            // Pop the type off the stack.
            LlvmType type = this.typeStack.Pop();

            // Create the LLVM alloca instruction.
            LlvmValue value = this.builder.CreateAlloca(type, node.ResultIdentifier);

            // Append the resulting value onto the stack.
            this.valueStack.Push(value);

            // Return the node.
            return node;
        }

    }
}
