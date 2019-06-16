using System;
using Ion.IR.Target;

namespace Ion.IR.Handling
{
    public interface IProvider<T>
    {
        T Target { get; }

        IrSymbolTable SymbolTable { get; }

        NoticeJar NoticeJar { get; }
    }

    public struct ProviderOptions<T>
    {
        public T Target { get; set; }

        public LlvmModule Module { get; set; }

        public NoticeJar ErrorJar { get; set; }
    }

    public class Provider<T> : IProvider<T>
    {
        public T Target { get; }

        public LlvmModule Module { get; }

        public NoticeJar NoticeJar { get; }

        public IrSymbolTable SymbolTable => this.Module.SymbolTable;

        public Provider(ProviderOptions<T> options)
        {
            // Ensure both target and module are provided.
            if (options.Target == null || options.Module == null)
            {
                throw new ArgumentNullException("Neither target nor module options may be null");
            }

            this.Target = options.Target;
            this.Module = options.Module;
            this.NoticeJar = options.ErrorJar ?? new NoticeJar();
        }

        public Provider(LlvmModule module, T target) : this(new ProviderOptions<T>
        {
            Module = module,
            Target = target
        })
        {
            //
        }
    }
}
