using Ion.Engine.Llvm;
using Ion.IR.Instructions;
using Ion.IR.Misc;

namespace Ion.IR.Llvm
{
    public static class LlvmBuilderExtension
    {
        public static LlvmValue Create(this LlvmBuilder builder, CallInstruction instruction)
        {
            // Create and return the instruction.
            return builder.CreateCall(instruction.Target, instruction.ResultIdentifier, instruction.Arguments.AsLlvmValues());
        }

        public static LlvmValue Create(this LlvmBuilder builder, CreateInstruction instruction)
        {
            // Create and return the instruction.
            return builder.CreateAlloca(instruction.Kind.AsLlvmType(), instruction.ResultIdentifier);
        }

        public static LlvmValue Create(this LlvmBuilder builder, SetInstruction instruction)
        {
            // Create and return the instruction.
            return builder.CreateStore(instruction.Value.AsLlvmValue(), instruction.Target.AsLlvmValue());
        }
    }
}
