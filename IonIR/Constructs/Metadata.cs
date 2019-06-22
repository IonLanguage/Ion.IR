namespace Ion.IR.Constructs
{
    public class Metadata : Construct
    {
        public override ConstructType ConstructType => ConstructType.Metadata;

        public string Key { get; }

        public string Value { get; }

        public Metadata(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"!'{this.Key}' '{this.Value}'";
        }
    }
}
