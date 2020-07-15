﻿using AhmedTrading.Data;
using System.Collections.Generic;

namespace AhmedTrading.Repository
{
    public interface IRegistrationRepository : IRepository<Registration>
    {
        int GetRegID_ByUserName(string UserName);
        ICollection<DDL> SubAdmins();
        ICollection<AdminInfo> GetSubAdminList();
        AdminBasic GetAdminBasic(string userName);
        AdminInfo GetAdminInfo(string userName);
        void UpdateCustom(string userName, AdminInfo reg);
    }
}