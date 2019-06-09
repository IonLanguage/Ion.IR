using Ion.IR.Constants;

namespace Ion.IR.Constructs
{
    public class Type : IConstruct
    {
        public string Name { get; }

        public bool IsPointer { get; }

        public Type(string name, bool isPointer = false)
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
