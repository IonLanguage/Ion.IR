using System;
using LLVMSharp;

namespace Ion.IR.Target
{
    public static class LlvmConstFactory
    {
        public static LlvmValue Int(LlvmType type, long value)
        {
            // Create the value. Must be an unsigned integer.
            ulong transformedValue = (ulong)Math.Abs(value);

            // Determine whether to flip to negative or stay in positive.
            bool flip = value < 0;

            // Create, wrap and return the constant.
            return LLVM.ConstInt(type.Unwrap(), transformedValue, flip).Wrap();
        }

        public static LlvmValue String(string value)
        {
            // Create, wrap and return the constant.
            return LLVM.ConstString(value, (uint)value.Length, false).Wrap();
        }

        public static LlvmValue Array(LlvmType elementType, LlvmValue[] values)
        {
            // Create, wrap and return the constant.
            return LLVM.ConstArray(elementType.Unwrap(), values.Unwrap()).Wrap();
        }
    }
}
