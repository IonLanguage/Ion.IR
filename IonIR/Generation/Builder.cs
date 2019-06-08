using Ion.IR.Constructs;

namespace Ion.IR.Generation
{
    public interface IBuilder<T>
    {
        T Build();
    }
}
