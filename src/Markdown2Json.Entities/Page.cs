using System;
using System.Collections.Generic;
using System.Text;

namespace Markdown2Json.Entities
{
    public class Page
    {
        public Index Index { get; set; }

        public string Header { get; set; }
        public string Content { get; set; }
        public PageType Type { get; set; }

        public Page NextPage { get; set; }
        public Page PreviousPage { get; set; }       
    }
}
