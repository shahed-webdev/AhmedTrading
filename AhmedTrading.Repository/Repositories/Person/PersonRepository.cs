using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DbResponse Add(PersonModel model)
        {
            try
            {
                if (IsPhoneExist(model.Phone)) return new DbResponse(false, "Person already exist");

                var person = new Person
                {
                    Name = model.Name,
                    Phone = model.Phone
                };

                Context.Person.Add(person);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<PersonDetailsModel> ListDataTable(DataRequest request)
        {
            return Context.Person.Include(p => p.PersonalLoan).Select(p => new PersonDetailsModel
            {
                PersonId = p.PersonId,
                Name = p.Name,
                Phone = p.Phone,
                LoanAmount = p.PersonalLoan.Sum(l => l.LoanAmount),
                ReturnAmount = p.PersonalLoan.Sum(l => l.ReturnAmount),
                RemainingAmount = p.PersonalLoan.Sum(l => l.RemainingAmount)
            }).ToDataResult(request);
        }

        public bool IsPhoneExist(string phone)
        {
            return Context.Person.Any(c => c.Phone == phone);
        }

        public bool IsPhoneExist(string phone, int updateId)
        {
            return Context.Person.Any(c => c.Phone == phone && c.PersonId != updateId);
        }

        public DbResponse<PersonDetailsModel> Details(int id)
        {
            try
            {
                var person = Context.Person
                    .Include(p => p.PersonalLoan)
                    .FirstOrDefault(p => p.PersonId == id);
                if (person == null) return new DbResponse<PersonDetailsModel>(false, "No Data Found");

                var personModel = new PersonDetailsModel
                {
                    PersonId = person.PersonId,
                    Name = person.Name,
                    Phone = person.Phone,
                    LoanAmount = person.PersonalLoan.Sum(p => p.LoanAmount),
                    ReturnAmount = person.PersonalLoan.Sum(p => p.ReturnAmount),
                    RemainingAmount = person.PersonalLoan.Sum(p => p.RemainingAmount)
                };
                return new DbResponse<PersonDetailsModel>(true, "Success") { Data = personModel };
            }
            catch (Exception e)
            {
                return new DbResponse<PersonDetailsModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                var person = Context.Person.Find(id);
                if (person == null) return new DbResponse(false, "No Data Found");
                if (Context.PersonalLoan.Any(p => p.PersonId == person.PersonId)) return new DbResponse(false, "Related Data Found");

                Context.Person.Remove(person);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public async Task<ICollection<PersonModel>> SearchAsync(string key)
        {
            return await Context.Person
                .Where(c => c.Name.Contains(key) || c.Phone.Contains(key))
                .Select(p =>
                    new PersonModel
                    {
                        PersonId = p.PersonId,
                        Name = p.Name,
                        Phone = p.Phone
                    }).Take(5).ToListAsync().ConfigureAwait(false);
        }
    }
}