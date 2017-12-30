namespace Markdown2Json.Test
{
    using Markdown2Json.Entities;
    using Markdown2Json.Services;
    using Markdown2Json.Test.Util;
    using System.Linq;
    using Xunit;

    public class ParserTest : TestBase
    {    
        [Fact]
        public void ParseText_section_only_header()
        {
            var target = new Parser();
            var actual = target.Parse("# Foo Bar\r\n").ToArray();

            var expected = new[]
            {
                new Page
                {
                    Type = PageType.Section,
                    Header = "Foo Bar"
                }
            };

            Assert.Equal(expected, actual, Comparers.Page);
        }

        [Fact]
        public void ParseText_subsubsection_only_header()
        {
            var target = new Parser();
            var actual = target.Parse("### Foo Bar\r\n").ToArray();

            var expected = new[]
            {
                new Page
                {
                    Type = PageType.SubSubSection,
                    Header = "Foo Bar"
                }
            };

            Assert.Equal(expected, actual, Comparers.Page);
        }

        [Fact]
        public void ParseText_with_content()
        {
            var target = new Parser();
            var actual = target.Parse("## Foo\r\nTschö mit ø").ToArray();

            var expected = new[]
            {
                new Page
                {
                    Type = PageType.SubSection,
                    Header = "Foo",
                    Content = "Tschö mit ø",
                }
            };

            Assert.Equal(expected, actual, Comparers.Page);
        }

        [Fact]
        public void ParseText_with_content_multiline()
        {
            var target = new Parser();
            var actual = target.Parse("## Foo\r\nTschö mit ø\r\n\r\n#### Bar\r\n * Unordered sub-list. ").ToArray();

            var expected = new[]
            {
                new Page
                {
                    Type = PageType.SubSection,
                    Header = "Foo",
                    Content = "Tschö mit ø",
                },
                new Page
                {
                    Type = PageType.Segment,
                    Header = "Bar",
                    Content = "* Unordered sub-list.",
                },
            };

            Assert.Equal(expected, actual, Comparers.Page);
        }

        [Theory]
        [InlineData("#", PageType.Section)]
        [InlineData("##", PageType.SubSection)]
        [InlineData("###", PageType.SubSubSection)]
        [InlineData("####", PageType.Segment)]
        public void ParseType(string data, PageType expected)
        {
            var actual = Parser.ParseType(data);
            Assert.Equal(expected, actual);
        }
    }
}
