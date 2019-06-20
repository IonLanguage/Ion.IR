using System.Collections.Generic;
using Ion.Engine.Llvm;
using Ion.IR.Constructs;

namespace Ion.IR.Misc
{
    public static class Util
    {
        public static LlvmValue[] AsLlvmValues(this Value[] values)
        {
            // Create the buffer list.
            List<LlvmValue> buffer = new List<LlvmValue>();

            // Loop through all the values.
            foreach (Value value in values)
            {
                // Append value to the buffer.
                buffer.Add(value.AsLlvmValue());
            }

            // Return the buffer as an array.
            return buffer.ToArray();
        }

        public static Reference AsReference(this string value)
        {
            return new Reference(value);
        }
    }
}
