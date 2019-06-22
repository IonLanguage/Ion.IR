#nullable enable

using System;
using System.Collections.Generic;
using Ion.Engine.Llvm;
using Ion.Engine.Misc;
using Ion.IR.Constructs;
using Ion.IR.Instructions;
using Ion.IR.Tracking;
using Ion.IR.Visitor;
using LLVMSharp;

namespace Ion.IR.Handling
{
    public interface IVisitable<TNode, TVisitor>
    {
        TNode Accept(TVisitor visitor);
    }

    public class LlvmVisitor
    {
        protected readonly IrSymbolTable symbolTable;

        protected Stack<LlvmBlock> blockStack;

        protected Stack<LlvmValue> valueStack;

        protected Stack<LlvmType> typeStack;

        protected LlvmBuilder builder;

        protected LlvmModule module;

        protected Dictionary<string, LlvmValue> namedValues;

        public LlvmVisitor(LlvmModule module, LlvmBuilder builder)
        {
            this.module = module;
            this.builder = builder;
            this.symbolTable = new IrSymbolTable(this.module);
            this.valueStack = new Stack<LlvmValue>();
            this.typeStack = new Stack<LlvmType>();
            this.blockStack = new Stack<LlvmBlock>();
            this.namedValues = new Dictionary<string, LlvmValue>();
        }

        public Construct Visit(IVisitable<Constructs.Construct, LlvmVisitor> node)
        {
            if (node != null)
            {
                return node.Accept(this);
            }

            // TODO
            return null;
        }

        public Construct VisitExtension(Construct node)
        {
            return node.VisitChildren(this);
        }

        public Construct Visit(Routine node)
        {
            // Ensure body was provided or created.
            if (node.Body == null)
            {
                throw new Exception("Unexpected function body to be null");
            }
            // Ensure prototype is set.
            else if (node.Prototype == null)
            {
                throw new Exception("Unexpected function prototype to be null");
            }
            // Ensure that body returns a value if applicable.
            else if (!node.Prototype.ReturnType.IsVoid && !node.Body.HasReturnExpr)
            {
                throw new Exception("Functions that do not return void must return a value");
            }

            // Emit the argument types.
            LlvmType[] arguments = node.Prototype.Arguments.Emit(context);

            // Visit the return type node.
            this.Visit(node.Prototype.ReturnType);

            // Pop off the return type off the stack.
            LlvmType returnType = this.typeStack.Pop();

            // Emit the function type.
            LlvmType type = LlvmTypeFactory.Function(returnType, arguments, node.Prototype.Arguments.Continuous);

            // Create the function.
            LlvmFunction function = this.module.CreateFunction(node.Prototype.Identifier, type);

            // Create the argument index counter.
            uint argumentIndexCounter = 0;

            // Name arguments.
            foreach (FormalArg formalArgument in node.Prototype.Arguments.Values)
            {
                // Retrieve the argument.
                LlvmValue argument = function.GetArgumentAt(argumentIndexCounter);

                // Name the argument.
                argument.SetName(formalArgument.Identifier);

                // Increment the index counter for next iteration.
                argumentIndexCounter++;
            }

            // Visit the body.
            this.Visit(node.Body);

            // Pop the body off the stack.
            LlvmBlock body = this.blockStack.Pop();

            // Position the body's builder at the beginning.
            body.Builder.PositionAtStart();

            // TODO: Missing support for native attribute emission.
            // Emit attributes as first-class instructions if applicable.
            foreach (Attribute attribute in node.Attributes)
            {
                // Emit the attribute onto the body's builder context.
                attribute.Emit(bodyContext);
            }

            // Ensures the function does not already exist
            if (this.symbolTable.functions.Contains(node.Prototype.Identifier))
            {
                throw new Exception($"A function with the identifier '{node.Prototype.Identifier}' already exists");
            }

            // Register the function on the symbol table.
            this.symbolTable.functions.Add(function);

            // Append the function onto the stack.
            this.valueStack.Push(function);

            // Return the node.
            return node;
        }

        public Construct Visit(CallInst node)
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

            // Ensure the function has been emitted.
            if (!this.symbolTable.functions.Contains(node.TargetIdentifier))
            {
                throw new Exception($"Call to a non-existent function named '{node.TargetIdentifier}' performed");
            }

            // Retrieve the target function.
            LlvmFunction target = this.symbolTable.functions[node.TargetIdentifier];

            // Ensure argument count is correct (with continuous arguments).
            if (target.ContinuousArgs && arguments.Count < target.ArgumentCount - 1)
            {
                throw new Exception($"Target function requires at least {target.ArgumentCount - 1} argument(s)");
            }
            // Otherwise, expect the argument count to be exact.
            else if (arguments.Count != target.ArgumentCount)
            {
                throw new Exception($"Argument amount mismatch, target function requires exactly {target.ArgumentCount} argument(s)");
            }

            // Create the function call.
            Instruction functionCall = new Instruction(this.Identifier, this.TargetIdentifier);

            // Append the value onto the stack.
            this.valueStack.Push(functionCall);

            // Return the node.
            return node;
        }

        public Construct Visit(Integer node)
        {
            // Push the node's llvm value to the stack.
            this.valueStack.Push(node.AsLlvmValue());

            // Return the node.
            return node;
        }

        public Construct Visit(Variable node)
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

        public Construct Visit(CallExpr node)
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
            foreach (Visitor.Construct argument in node.Arguments)
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

        public Construct Visit(Prototype node)
        {
            // Retrieve argument count within node.
            uint argumentCount = (uint)node.Arguments.Count;

            // Create the argument buffer array.
            LlvmType[] arguments = new LlvmType[Math.Max(argumentCount, 1)];

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
                // TODO: Wrong type.
                for (int i = 0; i < argumentCount; ++i)
                {
                    arguments[i] = LLVM.DoubleType().Wrap();
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

        public Construct Visit(Function node)
        {
            // Clear named values to reset scope upon processing a new function.
            this.namedValues.Clear();

            // Visit the function's prototype.
            this.Visit((Construct)node.Prototype);

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

        public Construct VisitBinaryExpr(BinaryExpr node)
        {
            // Visit left side.
            this.Visit(node.LeftSide);

            // Visit right side.
            this.Visit(node.RightSide);

            // Pop right side off the stack.
            LlvmValue rightSide = this.valueStack.Pop();

            // Pop left side off the stack.
            LlvmValue leftSide = this.valueStack.Pop();

            // Create a value buffer.
            LlvmValue binaryExpr;

            // TODO: Finish implementing.
            switch (node.Type)
            {
                case BinaryExprType.Addition:
                    {
                        binaryExpr = LLVM.
                }
            }
        }
    }
}
