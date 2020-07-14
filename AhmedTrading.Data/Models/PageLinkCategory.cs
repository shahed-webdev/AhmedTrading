using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class PageLinkCategory
    {
        public PageLinkCategory()
        {
            PageLink = new HashSet<PageLink>();
        }

        public int LinkCategoryId { get; set; }
        public string Category { get; set; }
        public string IconClass { get; set; }
        public int? Sn { get; set; }

        public virtual ICollection<PageLink> PageLink { get; set; }
    }
}
