using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ion.IR.Misc
{
    public class Taggable
    {
        protected List<string> tags;

        public Taggable()
        {
            this.tags = new List<string>();
        }

        public void Tag(string tag)
        {
            this.tags.Add(tag);
        }

        public bool ContainsTag(string tag)
        {
            return this.tags.Contains(tag);
        }

        public ReadOnlyCollection<string> GetTags()
        {
            return this.tags.AsReadOnly();
        }
    }
}
