using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IEmployeeService
    {
        Task<Tuple<IList<Employee>, bool>> SearchEmployeeByNameAsync(string input, int page);
        Task<Employee> GetEmployeeAsync(int id);
        Task AddEmployeeAsync(string firstName, string lastName, DateTime startingDate, float salary, int vacationDays, byte experience);
        Task EditEmployeeAsync(int id, string firstName, string lastName, float salary, int vacationDays, byte experience);
        Task RemoveEmployeeAsync(int id);
        Task<IList<Employee>> GetOfficeEmployeesAsync(int id);
        Task AssignEmployeeAsync(int eId, int oId);
        Task UnassignEmployeeAsync(int eId);
    }
}
