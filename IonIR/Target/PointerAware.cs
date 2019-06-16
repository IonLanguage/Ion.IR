using System;

namespace Ion.IR.Target
{
    public interface IPointerAware
    {
        IntPtr Pointer { get; }

        bool IsNull { get; }
    }

    public abstract class PointerAware
    {
        IntPtr Pointer { get; }

        public bool IsNull => LlvmUtil.IsPointerNull(this.Pointer);
    }
}
