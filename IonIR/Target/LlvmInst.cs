using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmInst : LlvmWrapper<LLVMValueRef>
    {
        public LlvmInst(LLVMValueRef source) : base(source)
        {
            //
        }

        public void InsertIntoBuilder(LlvmBuilder builder)
        {
            builder.InsertInstruction(this);
        }
    }
}
