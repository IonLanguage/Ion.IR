using System;
using Ion.IR.Constants;
using Ion.IR.Constructs;
using Ion.IR.Instructions;
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
                switch (instruction.Name)
                {
                    // Call.
                    case InstructionName.Call:
                        {
                            block.Builder.Create((CallInstruction)instruction);

                            break;
                        }

                    // Create.
                    case InstructionName.Create:
                        {
                            block.Builder.Create((CreateInstruction)instruction);

                            break;
                        }

                    // Set.
                    case InstructionName.Set:
                        {
                            block.Builder.Create((SetInstruction)instruction);

                            break;
                        }

                    // Unrecognized instruction name.
                    default:
                        {
                            throw new Exception($"Unrecognized instruction name: {instruction.Name}");
                        }
                }
            }
        }
    }
}
