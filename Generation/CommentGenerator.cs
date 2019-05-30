using Ion.IR.Constants;

namespace Ion.IR.Generation
{
    public class CommentGenerator : IGenerator
    {
        public string Text { get; }

        public CommentGenerator(string text)
        {
            this.Text = text;
        }

        public string Generate()
        {
            return $"{Symbols.SemiColon}{this.Text}";
        }
    }
}
