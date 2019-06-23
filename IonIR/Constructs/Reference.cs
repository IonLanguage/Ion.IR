using Ion.IR.Constants;
using Ion.IR.Handling;

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

        public override Construct Accept(LlvmVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}
