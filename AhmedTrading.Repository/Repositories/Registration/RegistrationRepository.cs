﻿using AhmedTrading.Data;
using System.Collections.Generic;
using System.Linq;

namespace AhmedTrading.Repository
{
    public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }
        public int GetRegID_ByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return -1;

            return Context.Registration.FirstOrDefault(r => r.UserName == userName).RegistrationId;
        }

        public ICollection<AdminInfo> GetSubAdminList()
        {
            return Context.Registration.Where(r => r.Type == "Sub-Admin").Select(r => new AdminInfo
            {
                UserName = r.UserName,
                Name = r.Name,
                RegistrationId = r.RegistrationId,
                Type = r.Type,
                FatherName = r.FatherName,
                Address = r.Address,
                DateofBirth = r.DateofBirth,
                Designation = r.Designation,
                Email = r.Email,
                Image = r.Image,
                NationalId = r.NationalId,
                Phone = r.Phone
            }).ToList();
        }

        public ICollection<DDL> SubAdmins()
        {
            return Context.Registration?.Where(r => r.Type == "Sub-Admin").Select(r =>
                new DDL { value = r.RegistrationId, label = r.Name + " (" + r.UserName + ")" }).ToList();
        }

        public AdminBasic GetAdminBasic(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            return Context.Registration.Where(r => r.UserName == userName).Select(r => new AdminBasic
            {
                Name = r.Name,
                Image = r.Image,
                RegistrationId = r.RegistrationId,
                Type = r.Type
            }).FirstOrDefault();
        }

        public AdminInfo GetAdminInfo(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            return Context.Registration.Where(r => r.UserName == userName).Select(r => new AdminInfo
            {
                RegistrationId = r.RegistrationId,
                Name = r.Name,
                UserName = r.UserName,
                Type = r.Type,
                Image = r.Image,
                FatherName = r.FatherName,
                Designation = r.Designation,
                DateofBirth = r.DateofBirth,
                NationalId = r.NationalId,
                Address = r.Address,
                Phone = r.Phone,
                Email = r.Email
            }).FirstOrDefault();
        }


        public void UpdateCustom(string userName, AdminInfo reg)
        {
            var r = Context.Registration.FirstOrDefault(u => u.UserName == userName);

            r.FatherName = reg.FatherName;
            r.Name = reg.Name;
            r.Phone = reg.Phone;
            r.Email = reg.Email;
            if (reg.Image != null)
                r.Image = reg.Image;
            r.Address = reg.Address;
            r.Designation = reg.Designation;
            r.NationalId = reg.NationalId;
            r.DateofBirth = reg.DateofBirth;

            Update(r);
        }

    }
}
