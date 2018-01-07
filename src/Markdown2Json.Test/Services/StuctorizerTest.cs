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

            firstPage.Index = new Index(1, 0, 0, 0);
            firstPage.NextPage = secondPage;
            secondPage.Index = new Index(1, 0, 0, 1);
            secondPage.PreviousPage = firstPage;
            secondPage.NextPage = thirdPage;
            thirdPage.Index = new Index(1, 1, 0, 0);
            thirdPage.PreviousPage = secondPage;

            var expected = new[] { firstPage, secondPage, thirdPage };

            Assert.Equal(expected, actual, Comparers.Page);
        }

        [Fact]
        public void CreatePageTree_simple()
        {
            var target = new Structorizer();

            var expected = new[]
            {
                new Page
                {
                    Type = PageType.Section,
                    Header = "Foo",
                },
                new Page
                {
                    Type = PageType.Section,
                    Header = "Bar",
                },
                new Page
                {
                    Type = PageType.Section,
                    Header = "Qux",
                },
            };

            var actual = target.CreatePageTree(expected);

            Assert.Equal(expected, actual, Comparers.Page);
        }

        [Fact]
        public void CreatePageTree_with_children()
        {
            var target = new Structorizer();

            var data = new[]
            {
                new Page
                {
                    Type = PageType.Section,
                    Header = "Foo",
                },
                new Page
                {
                    Type = PageType.SubSection,
                    Header = "Bar",
                },
                new Page
                {
                    Type = PageType.SubSection,
                    Header = "Baz",
                },
                new Page
                {
                    Type = PageType.Section,
                    Header = "Qux",
                },
            };

            var expected = new[]
            {
                new Page
                {
                    Type = PageType.Section,
                    Header = "Foo",
                    Children = new []
                    {
                        new Page
                        {
                            Type = PageType.SubSection,
                            Header = "Bar",
                        },
                        new Page
                        {
                            Type = PageType.SubSection,
                            Header = "Baz",
                        },
                    },
                },
                new Page
                {
                    Type = PageType.Section,
                    Header = "Qux",
                },
            };

            var actual = target.CreatePageTree(data);

            Assert.Equal(expected, actual, Comparers.Page);
        }
    }
}
