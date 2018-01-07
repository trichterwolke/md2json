namespace Markdown2Json.Entities
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{Header}")]
    public class Page
    {
        public Index Index { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public PageType Type { get; set; }

        public Page NextPage { get; set; }

        public Page PreviousPage { get; set; }

        public IList<Page> Children { get; set; }
    }
}
