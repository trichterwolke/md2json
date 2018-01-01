namespace Markdown2Json.Test.Util
{
    using Markdown2Json.Entities;
    using System.Collections.Generic;

    public class ComparerFactory
    {
        public IEqualityComparer<Page> Page
        {
            get
            {
                return new ObjectComparer<Page>()
                  .Add(x => x.Header)
                  .Add(x => x.Type)
                  .Add(x => x.Content)
                  .Add(x => x.NextPage?.Header, "NextHeader")
                  .Add(x => x.PreviousPage?.Header, "PreviousHeader")
                  .Add(x => x.Index, Index);
            }
        }

        public IEqualityComparer<Index> Index
        {
            get
            {
                return new ObjectComparer<Index>()
                  .Add(x => x.Section)
                  .Add(x => x.SubSection)
                  .Add(x => x.SubSubSection)
                  .Add(x => x.Segment);
            }
        }
    }
}
