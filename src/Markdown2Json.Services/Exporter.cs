namespace Markdown2Json
{
    using Markdown2Json.Entities;
    using Markdown2Json.Services;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Exporter
    {
        private readonly ISerializer serializer;
        private readonly IStructorizer structorizer;
        private readonly IParser parser;
        private readonly IFileService fileService;

        public static Exporter Create()
        {
            return new Exporter(
                new Serializer(),
                new Structorizer(),
                new Parser(),
                new FileService());
        }

        public Exporter(
            ISerializer serializer, 
            IStructorizer structorizer, 
            IParser parser, 
            IFileService fileService)
        {
            this.serializer = serializer;
            this.structorizer = structorizer;
            this.parser = parser;
            this.fileService = fileService;
        }

        public void Export(string source, string destination)
        {
            var text = File.ReadAllText(source);
            var pages = this.parser.Parse(text).ToList();
            this.structorizer.Structure(pages);

            ExportInOneFile(destination, pages);
            ExportInSeparateFiles(destination, pages);
        }

        public void ExportInSeparateFiles(string destination, IEnumerable<Page> pages)
        {
            string text = this.serializer.Serialize(pages);
            string path = Path.Combine(destination, "pages");

            this.fileService.CreateIfNotExists(path);

            foreach (var page in pages)
            {
                ExportFile(path, page);
            }

            ExportPageList(destination, pages);
        }

        internal void ExportPageList(string directory, IEnumerable<Page> pages)
        {
            string text = this.serializer.CreatePageList(pages);
            string path = Path.Combine(directory, "content.json");
            this.fileService.OverwriteFile(path, text);
        }

        internal void ExportFile(string directory, Page page)
        {
            string text = this.serializer.Serialize(page);
            string path = Path.Combine(directory, $"{page.Header}.json");
            this.fileService.OverwriteFile(path, text);
        }

        internal void ExportInOneFile(string directory, IEnumerable<Page> pages)
        {
            string text = this.serializer.Serialize(pages);
            string path = Path.Combine(directory, "complete.json");
            this.fileService.OverwriteFile(path, text);
        }
    }
}
