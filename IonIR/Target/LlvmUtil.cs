using System;
using System.Collections.Generic;
using Ion.IR.Constructs;

namespace Ion.IR.Target
{
    public static class LlvmUtil
    {
        public static bool IsNull(IntPtr pointer)
        {
            return pointer == IntPtr.Zero;
        }

        public static T[] Unwrap<T>(this IWrapper<T>[] values)
        {
            // Create the buffer list.
            List<T> buffer = new List<T>();

            // Loop through all values.
            foreach (LlvmWrapper<T> value in values)
            {
                // Unwrap and append value to the buffer list.
                buffer.Add(value.Unwrap());
            }

            // Return the buffer as an array.
            return buffer.ToArray();
        }

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
    }
}
