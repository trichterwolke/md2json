namespace Markdown2Json.Services
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
                Ordering = FlattenOrdering(page.Ordering),
                page.Header,
                page.Content,
                Nextpage = CreateFlatPageReference(page.NextPage),
                PreviousPage = CreateFlatPageReference(page.PreviousPage),
            };
        }

        public object CreateFlatPageReference(Page page)
        {
            return page == null ? null : new
            {
                Ordering = FlattenOrdering(page.Ordering),
                page.Header,
            };
        }

        public string FlattenOrdering(Ordering ordering)
        {
            return $"{ordering.Section}.{ordering.SubSection}.{ordering.SubSubSection}.{ordering.Segment}";
        }

        public string CreatePageList(IEnumerable<Page> pages)
        {
            var flat = pages.Select(p => CreateFlatPageReference(p));
            return JsonConvert.SerializeObject(flat);
        }
    }
}
