using Ion.IR.Constructs;

namespace Ion.IR.Handling
{
    public interface IConstructHandler<TContext, TConstruct> where TConstruct : IConstruct
    {
        void Handle(TContext context, TConstruct construct);
    }
}
