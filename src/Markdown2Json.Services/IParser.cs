namespace Markdown2Json.Services
{
    using System.Collections.Generic;
    using Markdown2Json.Entities;

    public interface IParser
    {
        IEnumerable<Page> Parse(string text);
    }
}