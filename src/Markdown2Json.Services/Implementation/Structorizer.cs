namespace Markdown2Json.Services
{
    using Markdown2Json.Entities;
    using System.Collections.Generic;

    public class Structorizer : IStructorizer
    {
        public void Structure(IEnumerable<Page> pages)
        {
            Page lastPage = null;

            foreach (var page in pages)
            {
                if (lastPage == null)
                {
                    // first page
                    page.Index = new Index(1, 0, 0, 0);
                }
                else
                {                    
                    lastPage.NextPage = page;
                    page.PreviousPage = lastPage;
                    page.Index = lastPage.Index.CreateNext(page.Type);
                }

                lastPage = page;
            }
        }

        public IEnumerable<Page> CreatePageTree(IList<Page> pages)
        {
            int index = 0;
            return ParseChildren(ref index, pages);
        }

        public IList<Page> ParseChildren(ref int index, IList<Page> pages)
        {
            var result = new List<Page>();
            var currentPage = pages[index];
            result.Add(currentPage);
            index++;

            while(index < pages.Count)
            {
                var page = pages[index];

                if (page.Type == currentPage.Type)
                {
                    result.Add(page);
                    index++;
                }
                else if (page.Type > currentPage.Type)
                {
                    currentPage.Children = ParseChildren(ref index, pages);
                }
                else
                {
                    break;
                }
            }

            return result;
        }
    }
}

