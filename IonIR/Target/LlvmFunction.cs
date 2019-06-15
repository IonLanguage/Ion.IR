using System;
using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmFunction : LlvmWrapper<LLVMValueRef>, IVerifiable
    {
        public LlvmModule Parent { get; }

        public LlvmFunction(LlvmModule parent, LLVMValueRef source) : base(source)
        {
            // TODO: Need to verify source is a function.
            this.Parent = parent;
        }

        public LLVMGenericValueRef Run(LLVMGenericValueRef[] arguments)
        {
            // Delegate to the parent's execution engine.
            return this.Parent.ExecutionEngine.RunFunction(this, arguments);
        }

        public LLVMGenericValueRef Run()
        {
            // Delegate to the main handler with zero arguments.
            return this.Run(new LLVMGenericValueRef[] { });
        }

        public bool Verify()
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
