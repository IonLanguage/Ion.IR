using System.Collections.Generic;
using Ion.Engine.Llvm;

namespace Ion.IR.Tracking
{
    public class IrSymbolTable
    {
        public Dictionary<string, string> Metadata { get; }

        protected readonly LlvmModule module;

        public IrSymbolTable(LlvmModule module)
        {
            this.module = module;
            this.Metadata = new Dictionary<string, string>();
        }

        public bool ContainsFunction(string identifier)
        {
            // Delegate lookup to the symbol table's module.
            return this.module.ContainsFunction(identifier);
        }

        public LlvmFunction GetFunction(string identifier)
        {
            // Delegate lookup to the symbol table's module.
            return this.module.GetFunction(identifier);
        }
    }
}
