using Ion.IR.Constants;

namespace Ion.IR.Constructs
{
    public class Reference : Construct
    {
        public override ConstructType ConstructType => ConstructType.Id;

        public string Value { get; }

        public Reference(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{Symbol.IdPrefix}{this.Value}";
        }
    }
}
