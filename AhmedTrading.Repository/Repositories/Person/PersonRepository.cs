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

        public DataResult<PersonModel> ListDataTable(DataRequest request)
        {
            return Context.Person.Select(p => new PersonModel
            {
                PersonId = p.PersonId,
                Name = p.Name,
                Phone = p.Phone
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

        public DbResponse<PersonModel> Details(int id)
        {
            try
            {
                var p = Context.Person.Find(id);
                if (p == null) return new DbResponse<PersonModel>(false, "No Data Found");

                var person = new PersonModel
                {
                    PersonId = p.PersonId,
                    Name = p.Name,
                    Phone = p.Phone
                };
                return new DbResponse<PersonModel>(true, "Success") { Data = person };
            }
            catch (Exception e)
            {
                return new DbResponse<PersonModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                var p = Context.Person.Find(id);
                if (p == null) return new DbResponse(false, "No Data Found");

                Context.Person.Remove(p);
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