namespace Ion.IR.Handling
{
    public interface IProvider<T>
    {
        T Target { get; }
    }

    public class Provider<T>
    {
        public T Target { get; }

        public Provider(T target)
        {
            this.Target = target;
        }
    }
}
