namespace Markdown2Json.Test.Services
{
    using Markdown2Json.Services.Implementation;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    public class ReplacerTest
    {
        [Fact]
        public void Replace_h1()
        {
            var target = new Replacer();
            var data = "Foo\r\n====\r\nBar";

            var actual = target.ReplaceHeadlines(data);
            var expected = "# Foo\r\nBar";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Replace_h2()
        {
            var target = new Replacer();
            var data = "Foo\n---\nBar";

            var actual = target.ReplaceHeadlines(data);
            var expected = "## Foo\nBar";

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Replace_complex()
        {
            var target = new Replacer();
            var data = @"
# Qux
Lorem ipsum

Foo
===

### Baz
Loges dores

Bar
---

Doloris dones";

            var actual = target.ReplaceHeadlines(data);
            var expected = @"
# Qux
Lorem ipsum

# Foo

### Baz
Loges dores

## Bar

Doloris dones";

            Assert.Equal(expected, actual);
        }
    }
}
