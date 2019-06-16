using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ion.IR.ErrorHandling;

namespace Ion.IR.Handling
{
    public class NoticeJar
    {
        public ReadOnlyCollection<Error> Notices => this.errors.AsReadOnly();

        protected readonly List<Error> errors;

        public NoticeJar()
        {
            this.errors = new List<Error>();
        }

        public void Put(Error error)
        {
            this.errors.Add(error);
        }
    }
}
