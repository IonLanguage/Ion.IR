using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmValue : LlvmWrapper<LLVMValueRef>
    {
        public LlvmValue(LLVMValueRef source) : base(source)
        {
            //
        }

        public void SetName(string name)
        {
            LLVM.SetValueName(this.reference, name);
        }
    }
}
