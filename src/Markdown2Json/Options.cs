using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Markdown2Json
{
    class Options
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

        // Omitting long name, default --verbose
        [Option(
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        /*
        [Option(Default = "中文",
          HelpText = "Content language.")]
        public string Language { get; set; }

        [Value(0, MetaName = "offset",
          HelpText = "File offset.")]
        public long? Offset { get; set; }*/
    }
}
