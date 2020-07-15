using AhmedTrading.Data;
using System.Collections.Generic;

namespace AhmedTrading.Repository
{
    public interface IPageLinkAssignRepository : IRepository<PageLinkAssign>
    {
        ICollection<PageCategoryVM> SubAdminLinks(int regId);

        string AssignLink(int regId, ICollection<PageAssignVM> links);
    }
}
