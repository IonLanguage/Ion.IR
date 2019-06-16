using Ion.IR.Constructs;
using Ion.IR.Target;

namespace Ion.IR.Handling
{
    public class MetadataHandler : IConstructHandler<LlvmModule, Metadata>
    {
        public void Handle(IProvider<LlvmModule> provider, Metadata construct)
        {
            // Emit metadata onto the symbol table.
            provider.SymbolTable.Metadata.Add(construct.Key, construct.Value);
        }
    }
}
