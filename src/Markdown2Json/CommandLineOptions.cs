using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Markdown2Json
{
    public class CommandLineOptions
    {
        [Option('s',
            "source",
            Required = true,
            HelpText = "Input file to be processed.")]
        public string Source { get; set; }

        [Option('d',
            "destination",
            Required = true,
            HelpText = "Target directory where the json files are created.")]
        public string Destination { get; set; }

        [Option('u', "underline", HelpText = "Akzeptiert auch '=' und '-' um H1 und H2 zu untersuchen")]
        public bool IncludeUnderlineNotation { get; set; }

        [Option('l', "list", HelpText = "Erzeugt eine Datei mit der Auflistung aller Seiten")]
        public bool GeneratePagelist { get; set; }

        [Option('p', "pages", HelpText = "Erzeugt für jede Seite eine eigene Datei")]
        public bool GenerateSeperateFiles { get; set; }

        [Option('c', "complete", HelpText = "Erzeugt eine Datei die alle Seiten enthält")]
        public bool GenerateOneFile { get; set; }       
    }
}
