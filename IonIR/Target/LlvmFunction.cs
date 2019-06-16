using System;
using Ion.Engine.Tracking;
using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmFunction : LlvmValue, IVerifiable
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

        public ExitCode RunAsEntry(string[] arguments)
        {
            // Delegate to the parent's execution engine.
            return this.Parent.ExecutionEngine.RunFunctionAsEntry(this, arguments);
        }

        public ExitCode RunAsEntry()
        {
            // Delegate to the main handler with zero arguments.
            return this.RunAsEntry(new string[] { });
        }

        public bool Verify()
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
