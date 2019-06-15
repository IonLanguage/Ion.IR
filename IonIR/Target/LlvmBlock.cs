using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmBlock : LlvmWrapper<LLVMBasicBlockRef>
    {
        public LlvmFunction Parent { get; }

        public Builder Builder { get; }

        public LlvmBlock(LlvmFunction parent, LLVMBasicBlockRef source) : base(source)
        {
            this.Parent = parent;
            this.Builder = this.CreateBuilder();
        }

        public void SetName(string name)
        {
            LLVM.SetValueName(this.source, name);
        }

        public Builder CreateBuilder()
        {
            return new Builder(this);
        }

        public LlvmValue AsValue()
        {
            // Create a new value wrapper instance.
            return new LlvmValue(LLVM.BasicBlockAsValue(this.source));
        }
    }
}
