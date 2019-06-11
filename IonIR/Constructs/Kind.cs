using Ion.IR.Constants;

namespace Ion.IR.Constructs
{
    public class Kind : IConstruct
    {
        public ConstructType Type => ConstructType.Type;

        public string Name { get; }

        public bool IsPointer { get; }

        public Kind(string name, bool isPointer = false)
        {
            this.Name = name;
            this.IsPointer = isPointer;
        }

        public string Emit()
        {
            // Create the resulting string.
            string result = $"{Symbol.TypePrefix}{this.Name}";

            // Append the pointer symbol if applicable.
            if (this.IsPointer)
            {
                result += Symbol.Pointer;
            }

            // Return the result.
            return result;
        }
    }
}
