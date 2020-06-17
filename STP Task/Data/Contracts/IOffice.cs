using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contracts
{
    public interface IOffice
    {
        int Id { get; set; }
        string Country { get; set; }
        string City { get; set; }
        string Street { get; set; }
        int StreetNumber { get; set; }
        ICollection<Employee> Employees { get; set; }
        int CompanyId { get; set; }
        Company Company { get; set; }
        byte IsHeadquarter { get; set; }
        byte Removed { get; set; }
    }
}
