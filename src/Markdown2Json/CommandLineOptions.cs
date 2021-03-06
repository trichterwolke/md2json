﻿namespace Markdown2Json
{
    using CommandLine;

    public class CommandLineOptions
    {
        [Option('s', "source", Required = true, HelpText = "Input file to be processed.")]
        public string Source { get; set; }

        [Option('d', "destination", Required = true, HelpText = "Target directory where the json files are created.")]
        public string Destination { get; set; }

        [Option('u', "underline", HelpText = "Akzeptiert auch '=' und '-' um H1 und H2 zu untersuchen")]
        public bool IncludeUnderlineNotation { get; set; }

        [Option('l', "list", HelpText = "Erzeugt eine Datei mit der Auflistung aller Seiten")]
        public bool GeneratePageList { get; set; }

        [Option('t', "tree", HelpText = "Erzeugt eine Datei mit der hierarchischen Auflistung aller Seiten")]
        public bool GeneratePageTree { get; set; }

        [Option('p', "pages", HelpText = "Erzeugt für jede Seite eine eigene Datei")]
        public bool GenerateSeperateFiles { get; set; }

        [Option('c', "complete", HelpText = "Erzeugt eine Datei die alle Seiten enthält")]
        public bool GenerateCompleteFile { get; set; }

        [Option('g', "grave", HelpText = "Entfert den Gravis-Accent.")]
        public bool RemoveGrave { get; set; }

        [Option('h', "html", HelpText = "Konvertiert Markdown zu HTLM.")]
        public bool ConvertToHtml { get; set; }
    }
}
