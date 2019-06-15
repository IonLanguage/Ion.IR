using System.Collections.Generic;
using System.Collections.ObjectModel;
using LLVMSharp;

namespace Ion.IR.Target
{

    public class Builder : LlvmWrapper<LLVMBuilderRef>
    {
        public LlvmBlock Block { get; }

        public bool HasInstructions => this.GetFirstInstruction() != null;

        public ReadOnlyCollection<Inst> Instructions => this.instructions.AsReadOnly();

        protected readonly List<Inst> instructions;

        public Builder(LlvmBlock block, LLVMBuilderRef source) : base(source)
        {
            this.instructions = new List<Inst>();
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

        public Inst GetFirstInstruction()
        {
            // Attempt to retrieve the first instruction.
            LLVMValueRef firstInstruction = LLVM.GetFirstInstruction(this.Block.Unwrap());

            // No instructions.
            if (LlvmUtil.IsNull(firstInstruction.Pointer))
            {
                return null;
            }

            // Create and return an instruction wrapper instance.
            return new Inst(firstInstruction);
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

        public void InsertInstruction(Inst instruction)
        {
            // Register the instruction locally.
            this.instructions.Add(instruction);

            // Append the instruction to the source builder.
            LLVM.InsertIntoBuilder(this.source, instruction.Unwrap());
        }
    }
}
