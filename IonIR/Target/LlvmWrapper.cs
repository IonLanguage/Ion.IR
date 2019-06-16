using Ion.IR.Handling;

namespace Ion.IR.Target
{
    public interface IWrapper<T>
    {
        T Unwrap();
    }

    public class LlvmWrapper<T> : IWrapper<T>
    {
        protected T reference;

        public LlvmWrapper(T reference)
        {
            this.reference = reference;
        }

        public T Unwrap()
        {
            return this.reference;
        }

        public Router<T> CreateRouter(LlvmModule module)
        {
            return new Router<T>(module);
        }
    }
}
