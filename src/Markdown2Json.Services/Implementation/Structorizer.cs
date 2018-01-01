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
    }
}
