namespace Markdown2Json.Test
{
    using Markdown2Json.Entities;
    using Markdown2Json.Services;
    using Markdown2Json.Services.Implementation;
    using Xunit;

    public class PageSerializerTest
    {
        [Fact]
        public void Convert()
        {
            var target = new Serializer(s => s);
            var data = new Page
            {
                Type = PageType.SubSection,
                Index = new Index(1, 2, 3, 4),
                Header = "Foo",
                Content = "Tschö mit ø",
                NextPage = new Page
                {
                    Type = PageType.SubSection,
                    Index = new Index(1, 3, 0, 0),
                    Header = "Bar",
                    Content = "Irrelevant",
                }
            };

            string actual = target.Serialize(data);
            string expected = "{\"index\":\"1.2.3.4\",\"header\":\"Foo\",\"content\":\"Tschö mit ø\",\"nextPage\":{\"index\":\"1.3.0.0\",\"header\":\"Bar\"},\"previousPage\":null}";
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ConvertOrdering()
        {
            var target = new Serializer(s => s);           
            string actual = target.FlattenOrdering(new Index(1, 2, 3, 4));
            string expected = "1.2.3.4";
            Assert.Equal(expected, actual);
        }
    }
}
