using LLVMSharp;

namespace Ion.IR.Target
{
    public class Builder : LlvmWrapper<LLVMBuilderRef>
    {
        public Builder(LLVMBuilderRef source) : base(source)
        {
            //
        }

        public Builder() : this(LLVM.CreateBuilder())
        {
            //
        }
    }
}
