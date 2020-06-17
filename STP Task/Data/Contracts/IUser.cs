using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contracts
{
    public interface IUser
    {
        int Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
