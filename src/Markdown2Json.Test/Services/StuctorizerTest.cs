namespace Markdown2Json.Test
{
    using Markdown2Json.Entities;
    using Markdown2Json.Services;
    using Markdown2Json.Test.Util;
    using Xunit;

    public class StuctorizerTest : TestBase
    {
        [Fact]
        public void Structure()
        {
            var target = new Structorizer();
            var actual = new[]
            {
                new Page
                {
                    Type = PageType.Section,
                    Header = "Foo",
                    Content = "Lorem ipsum",
                },
                new Page
                {
                    Type = PageType.Segment,
                    Header = "Bar",
                    Content = "Dolori ipsum",
                },
                new Page
                {
                    Type = PageType.SubSection,
                    Header = "Qux",
                    Content = "Sentu ipsum",
                },
            };

            target.Structure(actual);


            var firstPage = new Page
            {
                Type = PageType.Section,
                Header = "Foo",
                Content = "Lorem ipsum",
            };

            var secondPage = new Page
            {
                Type = PageType.Segment,
                Header = "Bar",
                Content = "Dolori ipsum",
            };

            var thirdPage = new Page
            {
                Type = PageType.SubSection,
                Header = "Qux",
                Content = "Sentu ipsum",
            };

            firstPage.Ordering = new Ordering(1, 0, 0, 0);
            firstPage.NextPage = secondPage;
            secondPage.Ordering = new Ordering(1, 0, 0, 1);
            secondPage.PreviousPage = firstPage;
            secondPage.NextPage = thirdPage;
            thirdPage.Ordering = new Ordering(1, 1, 0, 0);
            thirdPage.PreviousPage = secondPage;

            var expected = new[] { firstPage, secondPage, thirdPage };

            Assert.Equal(expected, actual, Comparers.Page);                  
        }
    }
}
