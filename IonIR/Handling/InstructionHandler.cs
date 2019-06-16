using Ion.IR.Constructs;
using Ion.IR.Target;

namespace Ion.IR.Handling
{
    public abstract class InstructionHandler<T> : IConstructHandler<LlvmBuilder, T> where T : Instruction
    {
        public abstract void Handle(IProvider<LlvmBuilder> context, T instruction);
    }
}
