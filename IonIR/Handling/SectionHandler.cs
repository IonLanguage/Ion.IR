using System;
using Ion.Engine.Llvm;
using Ion.IR.Constants;
using Ion.IR.Constructs;
using Ion.IR.Instructions;

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
                            block.Builder.Create((CallInst)instruction);

                            break;
                        }

                    // Create.
                    case InstructionName.Create:
                        {
                            block.Builder.Create((CreateInst)instruction);

                            break;
                        }

                    // Set.
                    case InstructionName.Set:
                        {
                            block.Builder.Create((SetInst)instruction);

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
