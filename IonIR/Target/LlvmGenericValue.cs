using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmGenericValue : LlvmWrapper<LLVMGenericValueRef>
    {
        public LlvmGenericValue(LLVMGenericValueRef reference) : base(reference)
        {
        }
    }
}
