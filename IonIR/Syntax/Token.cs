namespace Ion.IR.Syntax
{
    public struct Token
    {
        public TokenType Type;

        public string Value;

        public int StartPos;

        /// <summary>
        /// The end position of this token.
        /// Returns the start position if the value is null.
        /// </summary>
        public int EndPos
        {
            get
            {
                // No value is set.
                if (this.Value == null)
                {
                    return this.StartPos;
                }

                return this.StartPos + this.Value.Length;
            }
        }
    }
}
