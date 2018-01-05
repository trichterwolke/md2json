namespace Markdown2Json
{
    using CommandLine;
    using System;
    using System.Collections.Generic;
    using Markdown2Json;
    using System.IO;
    using Markdown2Json.Entities;

    public class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<CommandLineOptions>(args);
            result.WithNotParsed(ShowError);
            result.WithParsed(CreateJson);
        }

        static void ShowError(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error);
                //Console.Read();
            }
        }

        static void CreateJson(CommandLineOptions options)
        {
            var source = new FileInfo(options.Source);
            if (!source.Exists)
            {
                Console.WriteLine($"File {source} not exists");
                return;
            }

            var exporterOptions = ParseExporterOptions(options);
            if (!ValidateExporterOptions(exporterOptions))
            {
                //Console.Read();
                return;
            }

            try
            {
                Exporter.Create(exporterOptions).Export(options.Source, options.Destination);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Console.WriteLine("Press any key");
                //Console.Read();
            }
        }

        public static bool ValidateExporterOptions(ExporterOptions options)
        {
            if (!options.IsGeneretorSelected())
            {
                Console.WriteLine("Es muss mindestens eine Exportoption angegeben werden.\nFür Hilfe --help angeben.");
                return false;
            }
            return true;
        }

        public static ExporterOptions ParseExporterOptions(CommandLineOptions options)
        {
            ExporterOptions result = ExporterOptions.None;

            if (options.GenerateCompleteFile)
            {
                result |= ExporterOptions.GenerateCompleteFile;
            }

            if (options.GeneratePagelist)
            {
                result |= ExporterOptions.GeneratePagelist;
            }

            if (options.GenerateSeperateFiles)
            {
                result |= ExporterOptions.GenerateSeperateFiles;
            }

            if (options.IncludeUnderlineNotation)
            {
                result |= ExporterOptions.IncludeUnderlineNotation;
            }

            if (options.RemoveGrave)
            {
                result |= ExporterOptions.RemoveGravis;
            }

            if (options.ConvertToHtml)
            {
                result |= ExporterOptions.ConvertToHtml;
            }

            return result;
        }
    }
}
