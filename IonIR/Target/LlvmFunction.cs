using System.Collections.Generic;
using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmFunction : LlvmValue, IVerifiable
    {
        public uint ArgumentCount => LLVM.CountParams(this.reference);

        public bool HasBlocks => this.NativeCountBlocks() > 0;

        public bool Deleted { get; protected set; }

        public LLVMLinkage Linkage => LLVM.GetLinkage(this.reference);

        public LlvmModule Parent { get; }

        protected readonly Dictionary<string, LlvmBlock> blocks;

        public LlvmFunction(LlvmModule parent, LLVMValueRef reference) : base(reference)
        {
            this.Deleted = false;

            // TODO: Need to verify reference is a function.
            this.Parent = parent;

            // Retrieve, wrap and convert blocks to a dictionary.
            this.blocks = LLVM.GetBasicBlocks(this.reference)
                .Wrap<LlvmBlock, LLVMBasicBlockRef>((LLVMBasicBlockRef value) => new LlvmBlock(this, value))
                .ToDictionary<LlvmBlock>();
        }

        public uint NativeCountBlocks()
        {
            return LLVM.CountBasicBlocks(this.reference);
        }

        public void Delete()
        {
            // Delete the function.
            LLVM.DeleteFunction(this.reference);

            // Raise the deleted flag.
            this.Deleted = true;
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

        public bool Verify(LLVMVerifierFailureAction failureAction)
        {
            // Verify the function.
            return LLVM.VerifyFunction(this.reference, failureAction).Value == 1;
        }

        public LlvmValue GetArgumentAt(uint index)
        {
            // Retrieve the parameter, wrap and return an llvm value instance.
            return LLVM.GetParam(this.reference, index).Wrap();
        }

        public void SetLinkage(LLVMLinkage linkage)
        {
            LLVM.SetLinkage(this.reference, linkage);
        }

        public bool Verify()
        {
            // Delegate to the main handler.
            return this.Verify(LLVMVerifierFailureAction.LLVMPrintMessageAction);
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
