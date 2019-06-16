using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmFunction : LlvmValue, IVerifiable
    {
        public LlvmModule Parent { get; }

        protected readonly Dictionary<string, LlvmBlock> blocks;

        public LlvmFunction(LlvmModule parent, LLVMValueRef reference) : base(reference)
        {
            // TODO: Need to verify reference is a function.
            this.Parent = parent;

            // Retrieve, wrap and convert blocks to a dictionary.
            this.blocks = LLVM.GetBasicBlocks(this.reference)
                .Wrap<LlvmBlock, LLVMBasicBlockRef>((LLVMBasicBlockRef value) => new LlvmBlock(this, value))
                .ToDictionary<LlvmBlock>();
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

        public LlvmBlock AppendBlock(string name)
        {
            // Append the basic block.
            LLVMBasicBlockRef block = LLVM.AppendBasicBlock(this.reference, name);

            // Wrap and return the reference.
            return new LlvmBlock(this, block);
        }
    }
}
