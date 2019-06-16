using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmInst : LlvmWrapper<LLVMValueRef>
    {
        public LlvmInst(LLVMValueRef reference) : base(reference)
        {
            //
        }

        public void InsertIntoBuilder(LlvmBuilder builder)
        {
            builder.InsertInstruction(this);
        }
    }
}
