using System.Collections.Generic;
using Markdown2Json.Entities;

namespace Markdown2Json.Services
{
    public interface IStructorizer
    {
        void Structure(IEnumerable<Page> pages);
    }
}