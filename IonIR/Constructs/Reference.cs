using Ion.IR.Constants;

namespace Ion.IR.Constructs
{
    public struct Reference : IConstruct
    {
        public ConstructType ConstructType => ConstructType.Id;

        public string Value { get; }

        public Reference(string value)
        {
            this.Value = value;
        }

        public string Emit()
        {
            return $"{Symbol.IdPrefix}{this.Value}";
        }
    }
}
