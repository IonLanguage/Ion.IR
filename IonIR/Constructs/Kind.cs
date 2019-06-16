using System;
using Ion.IR.Constants;
using Ion.IR.Target;

namespace Ion.IR.Constructs
{
    public class Kind : Construct
    {
        public override ConstructType ConstructType => ConstructType.Type;

        public string Name { get; }

        public bool IsPointer { get; }

        public Kind(string name, bool isPointer = false)
        {
            this.Name = name;
            this.IsPointer = isPointer;
        }

        public override string Emit()
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

        public LlvmType AsLlvmType()
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
