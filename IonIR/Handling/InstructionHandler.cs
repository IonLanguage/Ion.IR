using Ion.IR.Constructs;

namespace Ion.IR.Handling
{
    public abstract class InstructionHandler<T> : IConstructHandler<Target.Builder, T> where T : Instruction
    {
        public abstract void Handle(Target.Builder context, T instruction);
    }
}
