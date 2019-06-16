namespace Ion.IR.Target
{
    public class ExitCode
    {
        public const int Success = 0;

        public const int GenericFailure = -1;

        public bool IsGenericFailure => this.Value == ExitCode.GenericFailure;

        public bool IsFailure => !this.IsSuccess;

        public bool IsSuccess => this.Value == ExitCode.Success;

        public int Value { get; }

        public ExitCode(int value)
        {
            this.Value = value;
        }
    }
}
