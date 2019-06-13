namespace Ion.IR.Constructs
{
    public class Metadata : IConstruct
    {
        public ConstructType ConstructType => ConstructType.Metadata;

        public string Key { get; }

        public string Value { get; }

        public Metadata(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Emit()
        {
            return $"!'{this.Key}' '{this.Value}'";
        }
    }
}
