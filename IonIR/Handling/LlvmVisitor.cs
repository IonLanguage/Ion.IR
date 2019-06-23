#nullable enable

using System;
using System.Collections.Generic;
using Ion.Engine.Llvm;
using Ion.Engine.Misc;
using Ion.IR.Cognition;
using Ion.IR.Constants;
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
        public LlvmModule Module => this.module;

        protected readonly IrSymbolTable symbolTable;

        protected Stack<LlvmBlock> blockStack;

        protected Stack<LlvmValue> valueStack;

        protected Stack<LlvmType> typeStack;

        protected LlvmBuilder builder;

        protected LlvmModule module;

        protected LlvmFunction function;

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

        public LlvmVisitor(LlvmModule module) : this(module, LlvmBuilder.Create())
        {
            //
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

        public Construct VisitStoreInst(StoreInst node)
        {
            // Create the LLVM store instruction.
            LlvmValue value = this.builder.CreateStore(node.Value, node.Target);

            // Append the resulting instruction onto the stack.
            this.valueStack.Push(value);

            // Return the node.
            return node;
        }

        public Construct VisitValue(Value node)
        {
            // Create the value buffer.
            LlvmValue value;

            // Ensure value is identified as a literal.
            if (!Recognition.IsLiteral(node.Content))
            {
                throw new Exception("Content could not be identified as a valid literal");
            }
            // Integer literal.
            else if (Recognition.IsInteger(node.Content))
            {
                // Visit the kind.
                this.VisitKind(node.Kind);

                // Pop the resulting type off the stack.
                LlvmType type = this.typeStack.Pop();

                // Create the type and assign the value buffer.
                value = LlvmFactory.Int(type, int.Parse(node.Content));
            }
            // String literal.
            else if (Recognition.IsStringLiteral(node.Content))
            {
                value = LlvmFactory.String(node.Content);
            }
            // Unrecognized literal.
            else
            {
                throw new Exception($"Unrecognized literal: {node.Content}");
            }

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

        public Construct VisitKind(Kind node)
        {
            // Create the initial type.
            LlvmType type = TokenConstants.kindGenerationMap[node.Type]().Wrap();

            // Convert to a pointer if applicable.
            if (node.IsPointer)
            {
                type.ConvertToPointer();
            }

            // Append the resulting type onto the stack.
            this.typeStack.Push(type);

            // Return the node.
            return node;
        }

        public Construct VisitRoutine(Routine node)
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
            else if (!node.Prototype.ReturnKind.IsVoid && !node.Body.HasReturnValue)
            {
                throw new Exception("Functions that do not return void must return a value");
            }
            // Ensures the function does not already exist.
            else if (this.module.ContainsFunction(node.Prototype.Identifier))
            {
                throw new Exception($"A function with the identifier '{node.Prototype.Identifier}' already exists");
            }

            // Clear named values.
            this.namedValues.Clear();

            // Create an argument buffer list.
            List<LlvmType> arguments = new List<LlvmType>();

            // Process the prototype's arguments.
            foreach ((Kind kind, Reference reference) in node.Prototype.Arguments)
            {
                // Visit the argument's type.
                this.Visit(kind);

                // Pop the resulting type off the stack.
                LlvmType argumentType = this.typeStack.Pop();

                // Append the argument's type to the argument list.
                arguments.Add(argumentType);
            }

            // Visit the return type node.
            this.Visit(node.Prototype.ReturnKind);

            // Pop off the return type off the stack.
            LlvmType returnType = this.typeStack.Pop();

            // Emit the function type.
            LlvmType type = LlvmFactory.Function(returnType, arguments.ToArray(), node.Prototype.HasInfiniteArguments);

            // Create the function.
            LlvmFunction function = this.module.CreateFunction(node.Prototype.Identifier, type);

            // Register as the temporary, local function.
            this.function = function;

            // Create the argument index counter.
            uint argumentIndexCounter = 0;

            // Name arguments.
            foreach ((Kind kind, Reference reference) in node.Prototype.Arguments)
            {
                // Retrieve the argument.
                LlvmValue argument = function.GetArgumentAt(argumentIndexCounter);

                // Name the argument.
                argument.SetName(reference.Value);

                // Increment the index counter for next iteration.
                argumentIndexCounter++;
            }

            // Visit the body.
            this.VisitSection(node.Body);

            // Pop the body off the stack.
            this.blockStack.Pop();

            // Verify the function.
            function.Verify();

            // Append the function onto the stack.
            this.valueStack.Push(function);

            // Return the node.
            return node;
        }

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

        public Construct VisitInteger(Integer node)
        {
            // Visit the kind.
            this.VisitKind(node.Kind);

            // Pop the resulting type off the stack.
            LlvmType type = this.typeStack.Pop();

            // Convert to a constant and return as an llvm value wrapper instance.
            LlvmValue value = LlvmFactory.Int(type, node.Value);

            // Push the value onto the stack.
            this.valueStack.Push(value);

            // Return the node.
            return node;
        }

        public Construct VisitVariable(Variable node)
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

        public Construct VisitCallExpr(CallExpr node)
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
            foreach (Construct argument in node.Arguments)
            {
                this.Visit(argument);

                // Pop value from the stack and append to the buffer list.
                arguments.Add(this.valueStack.Pop());
            }

            // Create the call.
            LlvmValue call = this.builder.CreateCall(callee, Engine.Misc.SpecialName.Temporary, arguments.ToArray());

            // Push the call onto the stack.
            this.valueStack.Push(call);

            // Return the node.
            return node;
        }

        public Construct VisitPrototype(Prototype node)
        {
            // Retrieve argument count within node.
            uint argumentCount = (uint)node.Arguments.Length;

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
                // Create the function type.
                LlvmType type = LlvmFactory.Function(LlvmFactory.Void(), arguments, false);

                // Create the function within the module.
                function = this.module.CreateFunction(node.Identifier, type);

                // Set the function's linkage.
                function.SetLinkage(LLVMLinkage.LLVMExternalLinkage);
            }

            // Process arguments.
            for (int i = 0; i < argumentCount; ++i)
            {
                // Retrieve the argument name.
                string argumentName = node.Arguments[i].Item2.Value;

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

        public Construct VisitSection(Section node)
        {
            // Create the block.
            LlvmBlock block = this.function.AppendBlock(node.Identifier);

            // Process the section's instructions.
            foreach (Instruction inst in node.Instructions)
            {
                // Visit the instruction.
                this.Visit(inst);

                // Pop the resulting LLVM instruction off the stack.
                this.valueStack.Pop();
            }

            // Append the block onto the stack.
            this.blockStack.Push(block);

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
            LlvmValue binaryExpr = this.builder.CreateAdd(leftSide, rightSide, node.ResultIdentifier);

            // Push the resulting value onto the stack.
            this.valueStack.Push(binaryExpr);

            // Return the node.
            return node;
        }
    }
}
