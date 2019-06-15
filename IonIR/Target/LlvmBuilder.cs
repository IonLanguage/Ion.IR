using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LLVMSharp;

namespace Ion.IR.Target
{

    public class Builder : LlvmWrapper<LLVMBuilderRef>, IDisposable
    {
        public LlvmBlock Block { get; }

        public bool HasInstructions => this.GetFirstInstruction() != null;

        public ReadOnlyCollection<LlvmInst> Instructions => this.instructions.AsReadOnly();

        protected readonly List<LlvmInst> instructions;

        public Builder(LlvmBlock block, LLVMBuilderRef source) : base(source)
        {
            this.instructions = new List<LlvmInst>();
            this.PositionAtEnd();
        }

        public Builder(LlvmBlock block) : this(block, LLVM.CreateBuilder())
        {
            //
        }

        public void PositionAtEnd()
        {
            // Position the source builder at the end.
            LLVM.PositionBuilderAtEnd(this.source, this.Block.Unwrap());
        }

        public LlvmInst GetFirstInstruction()
        {
            // Attempt to retrieve the first instruction.
            LLVMValueRef firstInstruction = LLVM.GetFirstInstruction(this.Block.Unwrap());

            // No instructions.
            if (LlvmUtil.IsNull(firstInstruction.Pointer))
            {
                return null;
            }

            // Create and return an instruction wrapper instance.
            return new LlvmInst(firstInstruction);
        }

        public void PositionAtStart()
        {
            // Position before first instruction if applicable.
            if (this.HasInstructions)
            {
                // Position builder before the first instruction.
                LLVM.PositionBuilderBefore(this.source, this.GetFirstInstruction().Unwrap());
            }

            // No instructions exist, position at end.
            this.PositionAtEnd();
        }

        public void PositionAt(LlvmInst instruction)
        {
            LLVM.PositionBuilder(this.source, this.Block.Unwrap(), instruction.Unwrap());
        }

        public void InsertInstruction(LlvmInst instruction)
        {
            // Register the instruction locally.
            this.instructions.Add(instruction);

            // Append the instruction to the source builder.
            LLVM.InsertIntoBuilder(this.source, instruction.Unwrap());
        }

        public LlvmValue CreateAdd(LlvmValue leftSide, LlvmValue rightSide, string resultIdentifier)
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildAdd(this.source, leftSide.Unwrap(), rightSide.Unwrap(), resultIdentifier);

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public LlvmValue CreateReturn(LlvmValue value)
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildRet(this.source, value.Unwrap());

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public LlvmValue CreateReturnVoid()
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildRetVoid(this.source);

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public void Dispose()
        {
            LLVM.DisposeBuilder(this.source);
        }
    }
}
