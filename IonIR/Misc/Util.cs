using System;
using System.Collections.Generic;
using System.Linq;
using Ion.Engine.Tracking;
using Ion.IR.Constructs;
using LLVMSharp;

namespace Ion.IR.Target
{
    public static class Util
    {
        public static bool IsPointerNull(IntPtr pointer)
        {
            return pointer == IntPtr.Zero;
        }

        public static LlvmValue Wrap(this LLVMValueRef reference)
        {
            return new LlvmValue(reference);
        }

        public static LlvmType Wrap(this LLVMTypeRef reference)
        {
            return new LlvmType(reference);
        }

        // TODO: Needs unit testing. Might not be able to cast TWrapper (because of constructor params).
        public static TWrapper[] Wrap<TWrapper, TValue>(this TValue[] values, Func<TValue, TWrapper> callback) where TWrapper : LlvmWrapper<TValue>
        {
            // Create the buffer list.
            List<TWrapper> buffer = new List<TWrapper>();

            // Loop through all values.
            foreach (TValue value in values)
            {
                // Invoke the callback and append to the buffer list.
                buffer.Add(callback(value));
            }

            // Return the buffer list as an array.
            return buffer.ToArray();
        }

        public static Dictionary<string, T> ToDictionary<T>(this T[] values) where T : INamed
        {
            return values.ToDictionary((value) => value.Name);
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

            // Return the buffer list as an array.
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
