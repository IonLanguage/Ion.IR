using Ion.IR.Constants;

namespace Ion.IR.Constructs
{
    public struct Id : IConstruct
    {
        public string Value { get; }

        public Id(string value)
        {
            this.Value = value;
        }

        public string Emit()
        {
            return $"{Symbol.IdPrefix}{this.Value}";
        }
    }
}
