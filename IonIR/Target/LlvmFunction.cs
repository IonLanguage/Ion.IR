using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmFunction : LlvmWrapper<LLVMValueRef>, IVerifiable
    {
        public LlvmFunction(LLVMValueRef source) : base(source)
        {
            // TODO: Need to verify source is a function.
        }

        public bool Verify()
        {

        }
    }
}
