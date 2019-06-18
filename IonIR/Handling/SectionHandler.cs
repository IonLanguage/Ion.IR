using Ion.IR.Constructs;
using Ion.IR.Target;

namespace Ion.IR.Handling
{
    public class SectionHandler : ConstructHandler<LlvmFunction, Section>
    {
        public override void Handle(Provider<LlvmFunction> provider, Section section)
        {
            // Append the block.
            LlvmBlock block = provider.Target.AppendBlock(section.Name);

            // Insert instructions.
            foreach (Instruction instruction in section.Instructions)
            {
                
            }
        }
    }
}
