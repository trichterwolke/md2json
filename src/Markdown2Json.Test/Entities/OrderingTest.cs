namespace Markdown2Json.Test.Entities
{
    using Markdown2Json.Entities;
    using Markdown2Json.Test.Util;
    using Xunit;

    public class OrderingTest : TestBase
    {
        [Fact]
        public void CreateNext_section()
        {
            var target = new Index(1, 2, 3, 4);
            var actual = target.CreateNext(PageType.Section);
            var expected = new Index(2, 0, 0, 0);

            Assert.Equal(expected, actual, Comparers.Index);
        }

        [Fact]
        public void CreateNext_subsection()
        {
            var target = new Index(1, 2, 3, 4);
            var actual = target.CreateNext(PageType.SubSection);
            var expected = new Index(1, 3, 0, 0);

            Assert.Equal(expected, actual, Comparers.Index);
        }

        [Fact]
        public void CreateNext_subsubsection()
        {
            var target = new Index(1, 2, 3, 4);
            var actual = target.CreateNext(PageType.SubSubSection);
            var expected = new Index(1, 2, 4, 0);

            Assert.Equal(expected, actual, Comparers.Index);
        }

        [Fact]
        public void CreateNext_segment()
        {
            var target = new Index(1, 2, 3, 4);
            var actual = target.CreateNext(PageType.Segment);
            var expected = new Index(1, 2, 3, 5);

            Assert.Equal(expected, actual, Comparers.Index);
        }
    }
}
