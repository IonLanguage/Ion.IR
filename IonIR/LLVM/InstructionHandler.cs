using Ion.IR.Constructs;

namespace Ion.IR.LLVM
{
    public abstract class InstructionHandler<T> : IConstructHandler<T> where T : Instruction
    {
        public abstract void Handle(T instruction);
    }
}
