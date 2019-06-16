using System;
using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmType : LlvmWrapper<LLVMTypeRef>, IPointerAware
    {
        public IntPtr Pointer => this.reference.Pointer;

        public bool IsNull => LlvmUtil.IsPointerNull(this.Pointer);

        public LlvmType(LLVMTypeRef reference) : base(reference)
        {
            //
        }

        public void ConvertToPointer()
        {
            this.reference = LLVM.PointerType(this.reference, 0);
        }
    }
}
