using System;

namespace Markdown2Json.Entities
{
   [Flags]
   public enum ExporterOptions : int
    {
        None = 0,
        IncludeUnderlineNotation = 1,
        GeneratePagelist = 2,
        GenerateSeperateFiles = 4,
        GenerateCompleteFile = 8,
        RemoveGravis = 16,
        ConvertToHtml = 32,
    }

    public static class ExporterOptionsExtensitons
    {
        public static bool IsGeneretorSelected(this ExporterOptions options)
        {
            return (options & (ExporterOptions.GenerateCompleteFile | ExporterOptions.GenerateSeperateFiles | ExporterOptions.GeneratePagelist)) != 0;
        }
    }
}
