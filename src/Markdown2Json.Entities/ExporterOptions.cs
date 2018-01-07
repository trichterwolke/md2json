using System;

namespace Markdown2Json.Entities
{
   [Flags]
   public enum ExporterOptions : int
    {
        None = 0,
        IncludeUnderlineNotation = 1,
        GeneratePageList = 2,
        GeneratePageTree = 4,
        GenerateSeperateFiles = 8,
        GenerateCompleteFile = 16,
        RemoveGravis = 32,
        ConvertToHtml = 64,
    }

    public static class ExporterOptionsExtensitons
    {
        public static bool IsGeneretorSelected(this ExporterOptions options)
        {
            return (options & (ExporterOptions.GenerateCompleteFile | ExporterOptions.GenerateSeperateFiles | ExporterOptions.GeneratePageList)) != 0;
        }
    }
}
