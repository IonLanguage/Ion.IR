using System;
using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmExecutionEngine : LlvmWrapper<LLVMExecutionEngineRef>, IDisposable
    {
        public LlvmExecutionEngine(LLVMExecutionEngineRef source) : base(source)
        {
            //
        }

        public LLVMGenericValueRef RunFunction(LlvmFunction function, LLVMGenericValueRef[] arguments)
        {
            // Run the function and capture its result.
            LLVMGenericValueRef result = LLVM.RunFunction(this.source, function.Unwrap(), arguments);

            // Return the result.
            return result;
        }

        public LLVMGenericValueRef RunFunction(LlvmFunction function)
        {
            // Delegate to the main handler with zero arguments.
            return this.RunFunction(function, new LLVMGenericValueRef[] { });
        }

        public ExitCode RunFunctionAsEntry(LlvmFunction function, string[] arguments)
        {
            // Avoid passing environment values as it may cause undefined behaviour.
            int exitCode = LLVM.RunFunctionAsMain(this.source, function.Unwrap(), (uint)arguments.Length, arguments, new string[] { });

            // Return the exit code wrapped in an exit code helper instance.
            return new ExitCode(exitCode);
        }

        public ExitCode RunFunctionAsEntry(LlvmFunction function)
        {
            // Delegate to the main handler with zero arguments.
            return this.RunFunctionAsEntry(function, new string[] { });
        }

        public void Dispose()
        {
            LLVM.DisposeExecutionEngine(this.source);
        }
    }
}
