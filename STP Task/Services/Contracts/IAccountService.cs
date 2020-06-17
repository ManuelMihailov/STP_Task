using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAccountService
    {
        Task AddAccountAsync(string userName, string password);
        Task<User> AttemptUserLoginAsync(string userName, string password);
        Task<User> FindUserByUserNameAsync(string username);

    }
}
