using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmModule : LlvmWrapper<LLVMModuleRef>
    {
        public LlvmExecutionEngine ExecutionEngine { get; }

        public LlvmModule(LLVMModuleRef source) : base(source)
        {
            this.ExecutionEngine = this.CreateExecutionEngine();
        }

        public void SetIdentifier(string identifier)
        {
            // Set the source's identifier.
            LLVM.SetModuleIdentifier(this.source, identifier, identifier.Length);
        }

        public LlvmExecutionEngine CreateExecutionEngine()
        {
            // Create the reference buffer.
            LLVMExecutionEngineRef reference;

            // TODO: Handle out error.
            LLVM.CreateExecutionEngineForModule(out reference, this.source, out _);

            // Create the execution engine wrapper.
            LlvmExecutionEngine executionEngine = new LlvmExecutionEngine(reference);

            // Return the wrapper.
            return executionEngine;
        }

        public void Dump()
        {
            LLVM.DumpModule(this.source);
        }
    }
}
