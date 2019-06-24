using System;
using Ion.Engine.Llvm;
using Ion.Engine.Misc;
using Ion.IR.Constructs;
using Ion.IR.Handling;
using Ion.IR.Misc;
using Ion.IR.Syntax;

namespace Ion.IR.Parsing
{
    public class Driver
    {
        // TODO: What if EOF token has not been processed itself?
        public bool HasNext => !this.stream.IsLastItem;

        protected readonly ParserContext context;

        protected readonly TokenStream stream;

        protected readonly LlvmModule module;

        protected readonly LlvmVisitor visitor;

        public Driver(LlvmModule module, TokenStream stream)
        {
            this.stream = stream;
            this.module = module;
            this.context = new ParserContext(this.stream);
            this.visitor = new LlvmVisitor(this.module);
        }

        public Driver(TokenStream stream) : this(LlvmModule.Create(SpecialName.Entry), stream)
        {
            //
        }

        public void Invoke()
        {
            while (this.HasNext)
            {
                this.Next();
            }
        }

        public bool Next()
        {
            // TODO: What if EOF token has not been processed itself?
            // End reached.
            if (this.stream.IsLastItem)
            {
                return false;
            }

            // Retrieve the current token.
            Token token = this.stream.Current;

            // Abstract the token's type.
            TokenType type = token.Type;

            // Create the buffer construct.
            Construct construct;

            // Routine definition.
            if (type == TokenType.SymbolAt)
            {
                construct = new RoutineParser().Parse(this.context);
            }
            // Otherwise, throw an error.
            else
            {
                throw new Exception("Unexpected top-level entity");
            }

            // Visit the construct.
            this.visitor.Visit(construct);

            return true;
        }
    }
}
