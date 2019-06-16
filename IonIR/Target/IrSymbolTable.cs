using System.Linq;

namespace Ion.IR.Target
{
    public class IrSymbolTable
    {
        protected readonly LlvmModule module;

        public IrSymbolTable(LlvmModule module)
        {
            this.module = module;
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
