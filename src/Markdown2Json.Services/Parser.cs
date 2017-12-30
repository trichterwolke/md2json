namespace Markdown2Json.Services
{
    using Markdown2Json.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Parser : IParser
    {
        public const string Pattern = "(?<type>#+) +(?<header>.*)[^.](?<content>[^#]*)";

        public IEnumerable<Page> Parse(string text)
        {
            var regex = new Regex(Pattern,RegexOptions.Multiline);
            foreach (Match match in regex.Matches(text))
            {
                string type = match.Groups["type"].Value?.Trim();
                string header = match.Groups["header"].Value?.Trim();
                string content = match.Groups["content"].Value?.Trim();

                yield return new Page
                {
                    Header = header,
                    Type = ParseType(type),
                    Content = string.IsNullOrEmpty(content) ? null : content,
                };
            }
        }

        public static PageType ParseType(string s)
        {
            switch (s)
            {
                case "#":
                    return PageType.Section;
                case "##":
                    return PageType.SubSection;
                case "###":
                    return PageType.SubSubSection;
                case "####":
                    return PageType.Segment;
                default:
                    return PageType.Segment;
            }
        }
    }
}