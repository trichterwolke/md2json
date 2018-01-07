namespace Markdown2Json
{
    using Markdig;
    using Markdown2Json.Entities;
    using Markdown2Json.Services;
    using Markdown2Json.Services.Implementation;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Exporter
    {
        private readonly ISerializer serializer;
        private readonly IStructorizer structorizer;
        private readonly IParser parser;
        private readonly IFileService fileService;
        private readonly IReplacer replacer;
        private readonly ExporterOptions options;

        public static Exporter Create(ExporterOptions options)
        {
            Func<string, string> contentConverter = s => s;

            if ((options & ExporterOptions.ConvertToHtml) == ExporterOptions.ConvertToHtml)
            {
                contentConverter = s => Markdown.ToHtml(s ?? string.Empty);
            }           

            return new Exporter(
                new Serializer(contentConverter),
                new Structorizer(),
                new Parser(),
                new FileService(),
                new Replacer(),
                options);
        }

        public Exporter(
            ISerializer serializer,
            IStructorizer structorizer,
            IParser parser,
            IFileService fileService,
            IReplacer replacer,
            ExporterOptions options)
        {
            this.serializer = serializer;
            this.structorizer = structorizer;
            this.parser = parser;
            this.fileService = fileService;
            this.replacer = replacer;
            this.options = options;
        }

        public void Export(string source, string destination)
        {
            if (this.options.IsGeneretorSelected())
            {
                this.fileService.CreateIfNotExists(destination);
            }

            string text = File.ReadAllText(source);

            if ((this.options & ExporterOptions.IncludeUnderlineNotation) == ExporterOptions.IncludeUnderlineNotation)
            {
                text = this.replacer.ReplaceHeadlines(text);
            }
            
            if ((this.options & ExporterOptions.RemoveGravis) == ExporterOptions.RemoveGravis)
            {
                text = this.replacer.RemoveGrave(text);
            }

            var pages = this.parser.Parse(text).ToList();
            this.structorizer.Structure(pages);

            if ((this.options & ExporterOptions.GenerateCompleteFile) == ExporterOptions.GenerateCompleteFile)
            {
                ExportCompleteFile(destination, pages);
            }

            if ((this.options & ExporterOptions.GenerateSeperateFiles) == ExporterOptions.GenerateSeperateFiles)
            {
                ExportInSeparateFiles(destination, pages);
            }

            if ((this.options & ExporterOptions.GeneratePageList) == ExporterOptions.GeneratePageList)
            {
                ExportPageList(destination, pages);
            }

            if ((this.options & ExporterOptions.GeneratePageTree) == ExporterOptions.GeneratePageTree)
            {
                ExportPageTree(destination, pages);
            }
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
        }

        internal void ExportPageList(string directory, IEnumerable<Page> pages)
        {
            string text = this.serializer.CreatePageOverview(pages);
            string path = Path.Combine(directory, "list.json");
            this.fileService.OverwriteFile(path, text);
        }

        internal void ExportPageTree(string directory, IList<Page> pages)
        {
            var tree = this.structorizer.CreatePageTree(pages);
            string text = this.serializer.CreatePageOverview(tree);
            string path = Path.Combine(directory, "tree.json");
            this.fileService.OverwriteFile(path, text);
        }

        internal void ExportFile(string directory, Page page)
        {
            string text = this.serializer.Serialize(page);
            string path = Path.Combine(directory, $"{this.serializer.FlattenOrdering(page.Index)}.json");
            this.fileService.OverwriteFile(path, text);
        }

        internal void ExportCompleteFile(string directory, IEnumerable<Page> pages)
        {
            string text = this.serializer.Serialize(pages);
            string path = Path.Combine(directory, "complete.json");
            this.fileService.OverwriteFile(path, text);
        }
    }
}
