using System.Collections.Generic;
using Markdown2Json.Entities;

namespace Markdown2Json.Services
{
    public interface ISerializer
    {
        string Serialize(IEnumerable<Page> pages);
        string Serialize(Page page);
        string CreatePageOverview(IEnumerable<Page> pages);
        //string CreatePageTree(IEnumerable<Page> pages);
        string FlattenOrdering(Index ordering);
    }
}