using LLVMSharp;

namespace Ion.IR.Target
{
    public class Inst : LlvmWrapper<LLVMValueRef>
    {
        public Inst(LLVMValueRef source) : base(source)
        {
            //
        }

        public void InsertIntoBuilder(Builder builder)
        {
            builder.InsertInstruction(this);
        }
    }
}
