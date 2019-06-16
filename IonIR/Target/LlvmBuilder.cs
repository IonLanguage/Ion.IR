using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LLVMSharp;

namespace Ion.IR.Target
{

    public class LlvmBuilder : LlvmWrapper<LLVMBuilderRef>, IDisposable
    {
        public LlvmBlock Block { get; }

        public bool HasInstructions => this.GetFirstInstruction() != null;

        public ReadOnlyCollection<LlvmInst> Instructions => this.instructions.AsReadOnly();

        protected readonly List<LlvmInst> instructions;

        public LlvmBuilder(LlvmBlock block, LLVMBuilderRef reference) : base(reference)
        {
            this.instructions = new List<LlvmInst>();
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

        public LlvmInst GetFirstInstruction()
        {
            // Attempt to retrieve the first instruction.
            LLVMValueRef firstInstruction = LLVM.GetFirstInstruction(this.Block.Unwrap());

            // No instructions.
            if (Util.IsPointerNull(firstInstruction.Pointer))
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
                LLVM.PositionBuilderBefore(this.reference, this.GetFirstInstruction().Unwrap());
            }

            // No instructions exist, position at end.
            this.PositionAtEnd();
        }

        public void PositionAt(LlvmInst instruction)
        {
            LLVM.PositionBuilder(this.reference, this.Block.Unwrap(), instruction.Unwrap());
        }

        public void InsertInstruction(LlvmInst instruction)
        {
            // Register the instruction locally.
            this.instructions.Add(instruction);

            // Append the instruction to the reference builder.
            LLVM.InsertIntoBuilder(this.reference, instruction.Unwrap());
        }

        public LlvmValue CreateAdd(LlvmValue leftSide, LlvmValue rightSide, string resultIdentifier)
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildAdd(this.reference, leftSide.Unwrap(), rightSide.Unwrap(), resultIdentifier);

            // Register the instruction.
            this.instructions.Add(new LlvmInst(reference));

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public LlvmValue CreateAlloca(LlvmType type, string resultIdentifier)
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildAlloca(this.reference, type.Unwrap(), resultIdentifier);

            // Register the instruction.
            this.instructions.Add(new LlvmInst(reference));

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public LlvmValue CreateCall(LlvmFunction function, string resultIdentifier, LlvmValue[] arguments)
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildCall(this.reference, function.Unwrap(), arguments.Unwrap(), resultIdentifier);

            // Register the instruction.
            this.instructions.Add(new LlvmInst(reference));

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
            this.instructions.Add(new LlvmInst(reference));

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public LlvmValue CreateReturnVoid()
        {
            // Invoke the native function and capture the resulting reference.
            LLVMValueRef reference = LLVM.BuildRetVoid(this.reference);

            // Register the instruction.
            this.instructions.Add(new LlvmInst(reference));

            // Wrap and return the reference.
            return new LlvmValue(reference);
        }

        public void Dispose()
        {
            LLVM.DisposeBuilder(this.reference);
        }
    }
}
