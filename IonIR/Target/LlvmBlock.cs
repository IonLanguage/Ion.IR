using Ion.Engine.Tracking;
using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmBlock : LlvmWrapper<LLVMBasicBlockRef>, INamed
    {
        public LlvmFunction Parent { get; }

        public LlvmBuilder Builder { get; }

        public string Name { get; protected set; }

        public LlvmBlock(LlvmFunction parent, LLVMBasicBlockRef reference) : base(reference)
        {
            this.Parent = parent;
            this.Builder = this.CreateBuilder();
            this.Name = LLVM.GetBasicBlockName(reference);
        }

        public void SetName(string name)
        {
            // Set the block's name.
            LLVM.SetValueName(this.reference, name);

            // Save the name locally.
            this.Name = name;
        }

        public LlvmBuilder CreateBuilder()
        {
            return new LlvmBuilder(this);
        }

        public LlvmValue AsValue()
        {
            // Create a new value wrapper instance.
            return new LlvmValue(LLVM.BasicBlockAsValue(this.reference));
        }
    }
}
