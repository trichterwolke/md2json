
namespace Markdown2Json.Test
{
    using Markdown2Json.Entities;
    using Markdown2Json.Services;
    using Xunit;

    public class PageSerializerTest
    {
        [Fact]
        public void Convert()
        {
            var target = new Serializer();
            var data = new Page
            {
                Type = PageType.SubSection,
                Ordering = new Ordering(1, 2, 3, 4),
                Header = "Foo",
                Content = "Tschö mit ø",
                NextPage = new Page
                {
                    Type = PageType.SubSection,
                    Ordering = new Ordering(1, 3, 0, 0),
                    Header = "Bar",
                    Content = "Irrelevant",
                }
            };

            string actual = target.Serialize(data);
            string expected = "{\"Ordering\":\"1.2.3.4\",\"Header\":\"Foo\",\"Content\":\"Tschö mit ø\",\"Nextpage\":{\"Ordering\":\"1.3.0.0\",\"Header\":\"Bar\"},\"PreviousPage\":null}";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertOrdering()
        {
            var target = new Serializer();           
            string actual = target.FlattenOrdering(new Ordering(1, 2, 3, 4));
            string expected = "1.2.3.4";
            Assert.Equal(expected, actual);
        }
    }
}
