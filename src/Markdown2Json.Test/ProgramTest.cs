namespace Markdown2Json.Test
{
    using Markdown2Json;
    using Markdown2Json.Entities;
    using Markdown2Json.Test.Util;
    using Xunit;

    public class OrderingTest : TestBase
    {
        [Theory]
        [InlineData(ExporterOptions.None, false)]
        [InlineData(ExporterOptions.IncludeUnderlineNotation, false)]
        [InlineData(ExporterOptions.GenerateSeperateFiles, true)]
        [InlineData(ExporterOptions.GenerateCompleteFile | ExporterOptions.GeneratePagelist, true)]
        public void ValidateExporterOption(ExporterOptions options, bool expected)
        {
            var actual = Program.ValidateExporterOptions(options);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParesExporterOptions_GenerateCompleteFile()
        {
            var data = new CommandLineOptions
            {
                GenerateCompleteFile = true,
            };
            var actual = Program.ParseExporterOptions(data);
            var expected = ExporterOptions.GenerateCompleteFile;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParesExporterOptions_GenerateSeperateFiles()
        {
            var data = new CommandLineOptions
            {
                GenerateSeperateFiles = true,
            };
            var actual = Program.ParseExporterOptions(data);
            var expected = ExporterOptions.GenerateSeperateFiles;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParesExporterOptions_GeneratePagelist()
        {
            var data = new CommandLineOptions
            {
                GeneratePagelist = true,
            };
            var actual = Program.ParseExporterOptions(data);
            var expected = ExporterOptions.GeneratePagelist;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParesExporterOptions_IncludeUnderlineNotation()
        {
            var data = new CommandLineOptions
            {
                IncludeUnderlineNotation = true,
            };
            var actual = Program.ParseExporterOptions(data);
            var expected = ExporterOptions.IncludeUnderlineNotation;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParesExporterOptions_convert_to_html()
        {
            var data = new CommandLineOptions
            {
                ConvertToHtml = true,
            };
            var actual = Program.ParseExporterOptions(data);
            var expected = ExporterOptions.ConvertToHtml;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParesExporterOptions_none()
        {
            var data = new CommandLineOptions();
            var actual = Program.ParseExporterOptions(data);
            var expected = ExporterOptions.None;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParesExporterOptions_all()
        {
            var data = new CommandLineOptions
            {
                GenerateSeperateFiles = true,
                GenerateCompleteFile = true,
                GeneratePagelist = true,
                IncludeUnderlineNotation = true,
            };
            var actual = Program.ParseExporterOptions(data);
            var expected = ExporterOptions.GenerateSeperateFiles | ExporterOptions.GenerateCompleteFile | ExporterOptions.GeneratePagelist | ExporterOptions.IncludeUnderlineNotation;
            Assert.Equal(expected, actual);
        }
    }
}