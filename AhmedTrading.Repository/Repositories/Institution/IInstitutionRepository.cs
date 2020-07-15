using AhmedTrading.Data;

namespace AhmedTrading.Repository
{
    public interface IInstitutionRepository : IRepository<Institution>
    {
        void UpdateCustom(InstitutionVM model);
        InstitutionVM FindCustom();
        HomeVM HomeInfo();
    }
}
