using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmModule : LlvmWrapper<LLVMModuleRef>, IVerifiable, IDisposable
    {
        public LlvmExecutionEngine ExecutionEngine { get; }

        protected readonly Dictionary<string, LlvmFunction> functions;

        public LlvmModule(LLVMModuleRef source) : base(source)
        {
            this.functions = new Dictionary<string, LlvmFunction>();
            this.ExecutionEngine = this.CreateExecutionEngine();
        }

        public void SetIdentifier(string identifier)
        {
            // Set the source's identifier.
            LLVM.SetModuleIdentifier(this.reference, identifier, identifier.Length);
        }

        public LlvmExecutionEngine CreateExecutionEngine()
        {
            // Create the reference buffer.
            LLVMExecutionEngineRef reference;

            // TODO: Handle out error.
            LLVM.CreateExecutionEngineForModule(out reference, this.reference, out _);

            // Create the execution engine wrapper.
            LlvmExecutionEngine executionEngine = new LlvmExecutionEngine(reference);

            // Return the wrapper.
            return executionEngine;
        }

        public void Dump()
        {
            LLVM.DumpModule(this.reference);
        }

        public bool ContainsFunction(string identifier)
        {
            return this.functions.ContainsKey(identifier);
        }

        public void AddFunction(LlvmFunction function)
        {
            this.functions.Add(function.Identifier, function);
        }

        public LlvmFunction GetFunction(string identifier)
        {
            return this.functions[identifier];
        }

        public void Dispose()
        {
            LLVM.DisposeModule(this.reference);
        }

        public bool Verify()
        {
            // Verify the module.
            LLVMBool result = LLVM.VerifyModule(this.reference, LLVMVerifierFailureAction.LLVMAbortProcessAction, out _);

            // Return whether the verification succeeded.
            return result.Value == 0;
        }

        public override string ToString()
        {
            // Print the module onto a string pointer.
            IntPtr pointer = LLVM.PrintModuleToString(this.reference);

            // Resolve the string pointer.
            string result = Marshal.PtrToStringAnsi(pointer);

            // Return the result.
            return result;
        }
    }
}
