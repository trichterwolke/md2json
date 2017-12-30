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
                  .Add(x => x.Ordering, Ordering);
            }
        }

        public IEqualityComparer<Ordering> Ordering
        {
            get
            {
                return new ObjectComparer<Ordering>()
                  .Add(x => x.Section)
                  .Add(x => x.SubSection)
                  .Add(x => x.SubSubSection)
                  .Add(x => x.Segment);
            }
        }
    }
}
