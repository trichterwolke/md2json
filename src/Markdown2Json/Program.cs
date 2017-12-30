namespace Markdown2Json
{
    using CommandLine;
    using System;
    using System.Collections.Generic;
    using Markdown2Json;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            result.WithNotParsed(ShowError);
            result.WithParsed(CreateJson);
        }

        static void ShowError(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
        }

        static void CreateJson(Options options)
        {
            var source = new FileInfo(options.Source);
            if (!source.Exists)
            {
                Console.WriteLine($"File {source} not exists");
                return;
            }
            

            try
            {
                Exporter.Create().Export(options.Source, options.Destination);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Console.WriteLine("Press any key");
            //Console.Read();
        }     
    }
}
