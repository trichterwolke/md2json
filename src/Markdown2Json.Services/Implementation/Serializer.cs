namespace Markdown2Json.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Newtonsoft.Json;
    using Markdown2Json.Entities;

    public class Serializer : ISerializer
    {

        public string Serialize(IEnumerable<Page> pages)
        {
            return JsonConvert.SerializeObject(pages.Select(p => CreateFlatPage(p)));
        }

        public string Serialize(Page page)
        {
            var flat = CreateFlatPage(page);
            return JsonConvert.SerializeObject(flat);
        }

        public object CreateFlatPage(Page page)
        {
            return new
            {
                index = FlattenOrdering(page.Index),
                header = page.Header,
                content = page.Content,
                nextPage = CreateFlatPageReference(page.NextPage),
                previousPage = CreateFlatPageReference(page.PreviousPage),
            };
        }

        public object CreateFlatPageReference(Page page)
        {
            return page == null ? null : new
            {
                index = FlattenOrdering(page.Index),
                header = page.Header,
            };
        }

        public string FlattenOrdering(Index index)
        {
            return $"{index.Section}.{index.SubSection}.{index.SubSubSection}.{index.Segment}";
        }

        public string CreatePageList(IEnumerable<Page> pages)
        {
            var flat = pages.Select(p => CreateFlatPageReference(p));
            return JsonConvert.SerializeObject(flat);
        }
    }
}
