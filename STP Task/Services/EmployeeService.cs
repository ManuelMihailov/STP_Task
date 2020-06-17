using Data;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly STPTaskDatabaseContext dbContext;
        public EmployeeService(STPTaskDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Tuple<IList<Employee>, bool>> SearchEmployeeByNameAsync(string input, int page)
        {
            var employees = await dbContext.Employees
                .Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(input.ToLower()) && p.Removed == 0).Skip((page - 1) * 10)
                .ToListAsync();

            bool lastPage = true;
            if (employees.Count > 10)
            {
                lastPage = false;
            }
            return new Tuple<IList<Employee>, bool>(employees.Take(10).ToList(), lastPage);
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            var employee = await dbContext.Employees
                .Where(p => p.Id == id).Include(p => p.CurrentOffice)
                .FirstOrDefaultAsync();
            return employee;
        }

        public async Task EditEmployeeAsync(int id, string firstName, string lastName, float salary, int vacationDays, byte experience)
        {
            var employee = await GetEmployeeAsync(id);
            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Salary = salary;
            employee.VacationDays = vacationDays;
            employee.ExperienceLevel = experience;

            await dbContext.SaveChangesAsync();
        }

        public async Task AddEmployeeAsync(string firstName, string lastName, DateTime startingDate, float salary, int vacationDays, byte experience)
        {
            var employee = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                StartingDate = startingDate,
                Salary = salary,
                VacationDays = vacationDays,
                ExperienceLevel = experience,
                Removed = 0
            };

            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveEmployeeAsync(int id)
        {
            var employee = await GetEmployeeAsync(id);
            employee.CurrentOffice = null;
            employee.Removed = 1;
            await dbContext.SaveChangesAsync();
        }

        public async Task<IList<Employee>> GetOfficeEmployeesAsync(int id)
        {
            var employees = await dbContext.Employees.Where(p => p.Removed == 0 && (p.OfficeId == id || p.OfficeId == null)).ToListAsync();
            return employees;
        }

        public async Task AssignEmployeeAsync(int eId, int oId)
        {
            var employee = await GetEmployeeAsync(eId);
            var companyName = (await dbContext.Offices.Include(p => p.Company).FirstOrDefaultAsync(p => p.Id == oId)).Company.Name;
            employee.OfficeId = oId;
            employee.CompanyName = companyName;
            await dbContext.SaveChangesAsync();
        }
        public async Task UnassignEmployeeAsync(int eId)
        {
            var employee = await GetEmployeeAsync(eId);
            employee.OfficeId = null;
            employee.CompanyName = null;
            await dbContext.SaveChangesAsync();
        }
    }
}
