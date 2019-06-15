namespace Ion.IR.Target
{
    public class LlvmWrapper<T>
    {
        protected readonly T source;

        public LlvmWrapper(T source)
        {
            this.source = source;
        }

        public T Unwrap()
        {
            return this.source;
        }
    }
}
