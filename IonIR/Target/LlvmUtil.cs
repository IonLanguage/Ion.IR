using System;

namespace Ion.IR.Target
{
    public static class LlvmUtil
    {
        public static bool IsNull(IntPtr pointer)
        {
            return pointer == IntPtr.Zero;
        }
    }
}
