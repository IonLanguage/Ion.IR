using System;
using System.Collections.Generic;

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
    }
}
