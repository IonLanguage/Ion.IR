using System.Text;
using Ion.IR.Constructs;

namespace Ion.IR.Generation
{
    public interface IBuilder<T>
    {
        T Build();
    }

    public class IrBuilder : IBuilder<string>
    {
        protected readonly StringBuilder builder = new StringBuilder();

        public IrBuilder()
        {
            this.builder = new StringBuilder();
        }

        public void Emit(IConstruct construct)
        {
            this.builder.Append(construct.Emit());
        }

        public string Build()
        {
            return this.builder.ToString();
        }
    }
}
