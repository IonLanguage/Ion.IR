using Ion.IR.Constructs;

namespace Ion.IR.Handling
{
    public interface IConstructHandler<TContext, TConstruct> where TConstruct : IConstruct
    {
        void Handle(IProvider<TContext> provider, TConstruct construct);
    }

    public abstract class ConstructHandler<TContext, TConstruct> where TConstruct : IConstruct
    {
        public abstract void Handle(Provider<TContext> provider, TConstruct construct);
    }
}
