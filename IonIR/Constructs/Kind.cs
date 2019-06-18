using System;
using Ion.Engine.Tracking;
using Ion.IR.Constants;
using Ion.IR.Target;

namespace Ion.IR.Constructs
{
    public class Kind : Construct, INamed
    {
        public override ConstructType ConstructType => ConstructType.Type;

        public KindType Type { get; }

        public string Name { get; }

        public bool IsPointer { get; }

        public Kind(KindType type, bool isPointer = false)
        {
            // Ensure type is regitered in the reverse type map.
            if (!TokenConstants.kindReverseTypeMap.ContainsKey(type))
            {
                throw new Exception($"Unrecognized type: {type}");
            }

            this.Type = type;
            this.Name = TokenConstants.kindReverseTypeMap[type];
            this.IsPointer = isPointer;
        }

        public Kind(string name, bool isPointer = false) : this(TokenConstants.kindTypeMap[name], isPointer)
        {
            //
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
            // Create the initial type.
            LlvmType type = TokenConstants.kindGenerationMap[this.Type]().Wrap();

            // Convert to a pointer if applicable.
            if (this.IsPointer)
            {
                type.ConvertToPointer();
            }

            // Return the resulting type.
            return type;
        }
    }
}
