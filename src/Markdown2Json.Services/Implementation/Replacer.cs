namespace Markdown2Json.Services.Implementation
{
    using System.Text.RegularExpressions;

    public class Replacer : IReplacer
    {
        private const string Pattern = @"(?<header>.+?)(\n\r|\n|\r)(?<type>=+|-+)(?=\n\r|\n|\r)";

        public string ReplaceHeadlines(string text)
        {
            var regex = new Regex(Pattern, RegexOptions.ExplicitCapture);
            var evaluator = new MatchEvaluator(Replace);
            return regex.Replace(text, evaluator);
        }

        private string Replace(Match match)
        {
            string type = match.Groups["type"].Value?.Trim();
            string header = match.Groups["header"].Value?.Trim();

            return (type.StartsWith('=') ? "# " : "## ") + header;
        }
    }
}
