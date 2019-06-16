using Ion.IR.Constructs;

namespace Ion.IR.Handling
{
    public interface IConstructHandler<TContext, TConstruct> where TConstruct : IConstruct
    {
        void Handle(IProvider<TContext> provider, TConstruct construct);
    }
}
