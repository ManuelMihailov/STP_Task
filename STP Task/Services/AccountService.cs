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
    public class AccountService : IAccountService
    {
        private readonly IHasher hasher;
        private readonly STPTaskDatabaseContext dbContext;
        public AccountService(STPTaskDatabaseContext dbContext, IHasher hasher)
        {
            this.hasher = hasher;
            this.dbContext = dbContext;
        }
        public async Task AddAccountAsync(string userName, string password)
        {
            if (String.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Username cannot be null or empty.");
            if (String.IsNullOrWhiteSpace(password) || password.Length < 6)
                throw new ArgumentException("Password is required and must be at least 6 characters long.");
            if (await this.dbContext.Users.AnyAsync(p => p.Username == userName))
                throw new ArgumentException("Username is already taken.");

            var user = new User()
            {
                Username = userName,
                Password = hasher.Hash(password)
            };
            await dbContext.Users.AddAsync(user);
            dbContext.SaveChanges();
        }

        public async Task<User> AttemptUserLoginAsync(string userName, string password)
        {
            var user = await dbContext.Users
               .Where(p => p.Username == userName)
               .FirstOrDefaultAsync();
            if (user == null || !this.hasher.Verify(password, user.Password))
            {
                throw new ArgumentException("username or password incorrect");
            }
            return user;
        }

        public async Task<User> FindUserByUserNameAsync(string username)
        {
            var user = await dbContext.Users
                 .Where(p => p.Username == username)
                 .FirstOrDefaultAsync();
            if (user == null) throw new ArgumentNullException("No user found.");
            return user;
        }

    }
}
