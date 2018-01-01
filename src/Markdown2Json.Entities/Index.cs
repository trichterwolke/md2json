using System;
using System.Collections.Generic;
using System.Text;

namespace Markdown2Json.Entities
{
    public class Index
    {
        public Index(int section, int subSection, int subSubSection, int segment)
        {
            Section = section;
            SubSection = subSection;
            SubSubSection = subSubSection;
            Segment = segment;
        }

        public int Section { get; set; }

        public int SubSection { get; set; }

        public int SubSubSection { get; set; }

        public int Segment { get; set; }

        public Index CreateNext(PageType pageType)
        {
            switch (pageType)
            {
                case PageType.Section:
                    return new Index(Section + 1, 0, 0, 0);
                case PageType.SubSection:
                    return new Index(Section, SubSection + 1, 0, 0);
                case PageType.SubSubSection:
                    return new Index(Section, SubSection, SubSubSection + 1, 0);
                case PageType.Segment:
                    return new Index(Section, SubSection, SubSubSection, Segment + 1);
                default:
                    return null;
            }
        }
    }
}
