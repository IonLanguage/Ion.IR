using Ion.IR.Constructs;

namespace Ion.IR.LLVM
{
    public interface IConstructHandler<T> where T : IConstruct
    {
        void Handle(T construct);
    }
}
