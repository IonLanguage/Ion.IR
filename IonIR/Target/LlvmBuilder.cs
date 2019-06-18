using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ion.IR.Instructions;
using LLVMSharp;

namespace Ion.IR.Target
{

    public class LlvmBuilder : LlvmWrapper<LLVMBuilderRef>, IDisposable
    {
        public static LlvmBuilder CreateReference()
        {
            return new LlvmBuilder(null, LLVM.CreateBuilder());
        }

        public LlvmBlock Block { get; }

        public bool HasInstructions => this.GetFirstInstruction() != null;

        public ReadOnlyCollection<LlvmValue> Instructions => this.instructions.AsReadOnly();

        protected readonly List<LlvmValue> instructions;

        public LlvmBuilder(LlvmBlock block, LLVMBuilderRef reference) : base(reference)
        {
            this.instructions = new List<LlvmValue>();
            this.PositionAtEnd();
        }

        public LlvmBuilder(LlvmBlock block) : this(block, LLVM.CreateBuilder())
        {
            //
        }

        public void PositionAtEnd()
        {
            // Position the reference builder at the end.
            LLVM.PositionBuilderAtEnd(this.reference, this.Block.Unwrap());
        }

        public LlvmValue GetFirstInstruction()
        {
            // Attempt to retrieve the first instruction.
            LLVMValueRef firstInstruction = LLVM.GetFirstInstruction(this.Block.Unwrap());

            // No instructions.
            if (Util.IsPointerNull(firstInstruction.Pointer))
            {
                return null;
            }

            // Create and return an instruction wrapper instance.
            return firstInstruction.Wrap();
        }

        public void PositionAtStart()
        {
            // Position before first instruction if applicable.
            if (this.HasInstructions)
            {
                // Position builder before the first instruction.
                LLVM.PositionBuilderBefore(this.reference, this.GetFirstInstruction().Unwrap());
            }

            // No instructions exist, position at end.
            this.PositionAtEnd();
        }

        public void PositionAt(LlvmValue instruction)
        {
            LLVM.PositionBuilder(this.reference, this.Block.Unwrap(), instruction.Unwrap());
        }

        public void Insert(LlvmValue instruction)
        {
            // Register the instruction locally.
            this.instructions.Add(instruction);

            // Append the instruction to the reference builder.
            LLVM.InsertIntoBuilder(this.reference, instruction.Unwrap());
        }

        public LlvmValue Create(CallInstruction instruction)
        {
            // Create and return the instruction.
            return this.CreateCall(instruction.Target, instruction.ResultIdentifier, instruction.Arguments.AsLlvmValues());
        }

        public LlvmValue Create(CreateInstruction instruction)
        {
            // Create and return the instruction.
            return this.CreateAlloca(instruction.Kind.AsLlvmType(), instruction.ResultIdentifier);
        }

        public LlvmValue Create(SetInstruction instruction)
        {
            // Create and return the instruction.
            return this.CreateStore(instruction.Value.AsLlvmValue(), instruction.Target.AsLlvmValue());
        }

        public LlvmValue CreateStore(LlvmValue value, LlvmValue target)
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildStore(this.reference, value.Unwrap(), target.Unwrap());

            // Register the instruction.
            this.instructions.Add(reference.Wrap());

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public LlvmValue CreateAdd(LlvmValue leftSide, LlvmValue rightSide, string resultIdentifier)
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildAdd(this.reference, leftSide.Unwrap(), rightSide.Unwrap(), resultIdentifier);

            // Register the instruction.
            this.instructions.Add(reference.Wrap());

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public LlvmValue CreateAlloca(LlvmType type, string resultIdentifier)
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildAlloca(this.reference, type.Unwrap(), resultIdentifier);

            // Register the instruction.
            this.instructions.Add(reference.Wrap());

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public LlvmValue CreateCall(LlvmFunction function, string resultIdentifier, LlvmValue[] arguments)
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildCall(this.reference, function.Unwrap(), arguments.Unwrap(), resultIdentifier);

            // Register the instruction.
            this.instructions.Add(reference.Wrap());

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public LlvmValue CreateCall(LlvmFunction function, string resultIdentifier)
        {
            // Delegate to the main handler with zero arguments.
            return this.CreateCall(function, resultIdentifier, new LlvmValue[] { });
        }

        public LlvmValue CreateReturn(LlvmValue value)
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildRet(this.reference, value.Unwrap());

            // Register the instruction.
            this.instructions.Add(reference.Wrap());

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public LlvmValue CreateReturnVoid()
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildRetVoid(this.reference);

            // Register the instruction.
            this.instructions.Add(reference.Wrap());

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public void Dispose()
        {
            LLVM.DisposeBuilder(this.reference);
        }
    }
}
