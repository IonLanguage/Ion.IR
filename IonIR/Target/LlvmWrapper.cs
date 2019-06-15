namespace Ion.IR.Target
{
    public interface IWrapper<T>
    {
        T Unwrap();
    }

    public abstract class LlvmWrapper<T> : IWrapper<T>
    {
        protected readonly T reference;

        public LlvmWrapper(T source)
        {
            this.reference = source;
        }

        public T Unwrap()
        {
            return this.reference;
        }
    }
}
