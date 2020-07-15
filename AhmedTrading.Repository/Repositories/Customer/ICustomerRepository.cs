using AhmedTrading.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface ICustomerRepository : IRepository<Customer>, IAddCustom<CustomerAddUpdateViewModel>
    {
        ICollection<CustomerListViewModel> ListCustom();
        Task<bool> IsPhoneNumberExistAsync(string number, int id = 0);
        CustomerAddUpdateViewModel FindCustom(int id);
        CustomerProfileViewModel ProfileDetails(int id);
        void CustomUpdate(CustomerAddUpdateViewModel model);
        void UpdatePaidDue(int id);
        Task<ICollection<CustomerListViewModel>> SearchAsync(string key);
        double TotalDue();
        ICollection<CustomerDueViewModel> TopDue(int totalCustomer);
    }
}