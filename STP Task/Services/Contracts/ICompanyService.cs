using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICompanyService
    {
        Task<Tuple<IList<Company>, bool>> SearchCompanyByNameAsync(string input, int page);
        Task<Company> FindCompanyByIdAsync(int id);
        Task AddCompanyAsync(string name, DateTime dateTime);
        Task AddOfficeAsync(string country, string city, string street, string number, int companyId);
        Task<int> RemoveOfficeAsync(int id);
        Task<int> AssignHeadquarters(int id);
        Task<int> UnassignHeadquarters(int id);
        Task RemoveCompanyAsync(int id);
        Task EditCompany(int id, string name);
        Task<Office> GetOfficeAsync(int id);

    }
}
