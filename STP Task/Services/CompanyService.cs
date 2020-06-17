using Data;
using Data.Contracts;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CompanyService : ICompanyService
    {
        private readonly STPTaskDatabaseContext dbContext;
        public CompanyService(STPTaskDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Tuple<IList<Company>, bool>> SearchCompanyByNameAsync(string input, int page)
        {
            var companies = await dbContext.Companies
                .Where(p => p.Name.ToLower().Contains(input.ToLower()) && p.Removed == 0).Skip((page - 1) * 10)
                .ToListAsync();

            bool lastPage = true;
            if (companies.Count > 10)
            {
                lastPage = false;
            }
            return new Tuple<IList<Company>, bool>(companies.Take(10).ToList(), lastPage);
        }

        public async Task<Company> FindCompanyByIdAsync(int id)
        {
            var company = await dbContext.Companies.Where(p => p.Id == id && p.Removed == 0)
                .Include(p => p.Offices)
                .FirstAsync();
            return company;
        }

        public async Task AddCompanyAsync(string name, DateTime dateTime)
        {
            var company = new Company()
            {
                Name = name,
                CreationDate = dateTime,
                Offices = new List<Office>()
            };

            await dbContext.Companies.AddAsync(company);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddOfficeAsync(string country, string city, string street, string number, int companyId)
        {
            var office = new Office()
            {
                Country = country,
                City = city,
                CompanyId = companyId,
                Street = street,
                StreetNumber = int.Parse(number)
            };

            await dbContext.Offices.AddAsync(office);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Office> GetOfficeAsync(int id)
        {
            var office = await dbContext.Offices.Include(p => p.Company).Include(p => p.Employees).FirstAsync(p => p.Id == id);
            return office;
        }

        public async Task<int> RemoveOfficeAsync(int id)
        {
            var office = await GetOfficeAsync(id);
            foreach (var item in office.Employees)
            {
                item.OfficeId = null;
                item.CompanyName = null;
            }
            office.Removed = 1;
            await dbContext.SaveChangesAsync();
            return office.CompanyId;
        }

        public async Task<int> AssignHeadquarters(int id)
        {
            var office = await GetOfficeAsync(id);
            office.IsHeadquarter = 1;
            await dbContext.SaveChangesAsync();
            return office.CompanyId;
        }

        public async Task<int> UnassignHeadquarters(int id)
        {
            var office = await GetOfficeAsync(id);
            office.IsHeadquarter = 0;
            await dbContext.SaveChangesAsync();
            return office.CompanyId;
        }

        public async Task EditCompany(int id, string name)
        {
            var company = await FindCompanyByIdAsync(id);
            company.Name = name;
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveCompanyAsync(int id)
        {
            var company = await FindCompanyByIdAsync(id);
            foreach (var item in company.Offices)
            {
                await RemoveOfficeAsync(item.Id);
            }
            company.Removed = 1;
            await dbContext.SaveChangesAsync();
        }
    }
}
