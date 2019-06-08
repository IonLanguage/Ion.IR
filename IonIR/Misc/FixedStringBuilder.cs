using System.Text;

namespace Ion.IR.Misc
{
    public class FixedStringBuilder
    {
        protected readonly StringBuilder builder;

        public FixedStringBuilder()
        {
            this.builder = new StringBuilder();
        }

        public void Append(string text)
        {
            this.builder.Append($"{text}\n");
        }

        public override string ToString()
        {
            return this.builder.ToString();
        }
    }
}
