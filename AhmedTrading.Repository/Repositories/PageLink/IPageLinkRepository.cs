using AhmedTrading.Data;
using System.Collections.Generic;

namespace AhmedTrading.Repository
{
    public interface IPageLinkRepository : IRepository<PageLink>, IAddCustom<PageLinkViewModel>
    {
        List<SideMenuCategory> GetSideMenuByUser(string userName);
    }
}
