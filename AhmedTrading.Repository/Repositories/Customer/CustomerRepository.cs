using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ICollection<CustomerListViewModel> ListCustom()
        {
            var cList = Context.Customer.Select(c => new CustomerListViewModel
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                CustomerAddress = c.CustomerAddress,
                PhonePrimary = c.CustomerPhone.FirstOrDefault(p => p.IsPrimary == true).Phone,
                Due = c.Due,
                SignUpDate = c.InsertDate
            });

            return cList.ToList();
        }

        public DataResult<CustomerListViewModel> ListDataTable(DataRequest request)
        {
            var cList = Context.Customer.Select(c => new CustomerListViewModel
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                CustomerAddress = c.CustomerAddress,
                PhonePrimary = c.CustomerPhone.FirstOrDefault(p => p.IsPrimary == true).Phone,
                Due = c.Due,
                SignUpDate = c.InsertDate
            });

            return cList.ToDataResult(request);
        }

        public async Task<bool> IsPhoneNumberExistAsync(string number, int id = 0)
        {
            if (id == 0)
                return await Context.CustomerPhone.AnyAsync(c => c.Phone == number).ConfigureAwait(false);

            return await Context.CustomerPhone.AnyAsync(c => c.Phone == number && c.CustomerId != id).ConfigureAwait(false);
        }

        public CustomerAddUpdateViewModel FindCustom(int id)
        {
            var customerList = Context.Customer.Select(c => new CustomerAddUpdateViewModel
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                CustomerAddress = c.CustomerAddress,
                PhoneNumbers = c.CustomerPhone.Select(p => new CustomerPhoneViewModel
                {
                    CustomerPhoneId = p.CustomerPhoneId,
                    Phone = p.Phone,
                    IsPrimary = p.IsPrimary
                }).ToList(),

            });

            return customerList.FirstOrDefault(c => c.CustomerId == id);
        }

        public CustomerProfileViewModel ProfileDetails(int id)
        {
            var customer = Context.Customer
                .Include(c => c.Selling)
                .Select(c => new CustomerProfileViewModel
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    CustomerAddress = c.CustomerAddress,
                    PhoneNumbers = c.CustomerPhone.Select(p => new CustomerPhoneViewModel
                    {
                        CustomerPhoneId = p.CustomerPhoneId,
                        Phone = p.Phone,
                        IsPrimary = p.IsPrimary
                    }).ToList(),
                    SellingRecords = c.Selling.Select(s => new CustomerSellingViewModel
                    {
                        SellingId = s.SellingId,
                        SellingSn = s.SellingSn,
                        SellingAmount = s.SellingTotalPrice,
                        SellingPaidAmount = s.SellingPaidAmount,
                        SellingDiscountAmount = s.SellingDiscountAmount,
                        SellingDueAmount = s.SellingDueAmount,
                        SellingDate = s.SellingDate
                    }).ToList(),
                    SignUpDate = c.InsertDate,
                    SoldAmount = c.TotalAmount,
                    DiscountAmount = c.TotalDiscount,
                    DueAmount = c.Due,
                    ReceivedAmount = c.Paid
                });
            return customer.FirstOrDefault(c => c.CustomerId == id);
        }

        public void CustomUpdate(CustomerAddUpdateViewModel model)
        {
            var customer = Context.Customer.Include(c => c.CustomerPhone).FirstOrDefault(c => c.CustomerId == model.CustomerId);
            customer.CustomerAddress = model.CustomerAddress;
            customer.CustomerName = model.CustomerName;
            customer.CustomerPhone = model.PhoneNumbers.Select(p => new CustomerPhone
            {
                CustomerPhoneId = p.CustomerPhoneId.GetValueOrDefault(),
                Phone = p.Phone,
                IsPrimary = p.IsPrimary ?? false,
                InsertDate = DateTime.Now
            }).ToList();

            Context.Customer.Update(customer);
        }

        public void UpdatePaidDue(int id)
        {
            var customer = Find(id);

            var obj = Context.Selling.Where(s => s.CustomerId == customer.CustomerId).GroupBy(s => s.CustomerId).Select(s =>
                new
                {
                    TotalAmount = s.Sum(c => c.SellingTotalPrice),
                    TotalDiscount = s.Sum(c => c.SellingDiscountAmount),
                    Paid = s.Sum(c => c.SellingPaidAmount)
                }).FirstOrDefault();

            customer.TotalAmount = obj.TotalAmount;
            customer.TotalDiscount = obj.TotalDiscount;
            customer.Paid = obj.Paid;

            Update(customer);
        }

        public async Task<ICollection<CustomerListViewModel>> SearchAsync(string key)
        {
            return await Context.Customer.Include(c => c.CustomerPhone)
                .Where(c => c.CustomerName.Contains(key) || c.CustomerPhone.Any(p => p.Phone.Contains(key)))
                .Select(c =>
                  new CustomerListViewModel
                  {
                      CustomerId = c.CustomerId,
                      CustomerName = c.CustomerName,
                      CustomerAddress = c.CustomerAddress,
                      PhonePrimary = c.CustomerPhone.FirstOrDefault(p => p.Phone.Contains(key)).Phone,
                      Due = c.Due,
                      SignUpDate = c.InsertDate
                  }).Take(5).ToListAsync().ConfigureAwait(false);
        }

        public double TotalDue()
        {
            return Context.Customer?.Sum(s => s.Due) ?? 0;
        }

        public ICollection<CustomerDueViewModel> TopDue(int totalCustomer)
        {
            return Context.Customer.Where(c => c.Due > 0).OrderByDescending(c => c.Due).Take(totalCustomer).Select(c => new CustomerDueViewModel
            {
                CustomerId = c.CustomerId,
                Name = c.CustomerName,
                Due = c.Due
            }).ToList();
        }


        public void AddCustom(CustomerAddUpdateViewModel model)
        {
            var customer = new Customer
            {
                CustomerName = model.CustomerName,
                CustomerAddress = model.CustomerAddress,
                CustomerPhone = model.PhoneNumbers.Select(p => new CustomerPhone
                {
                    Phone = p.Phone,
                    IsPrimary = p.IsPrimary ?? false
                }).ToList()
            };
            Add(customer);
        }
    }
}