using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Office : IOffice
    {
        public Office()
        {
            Employees = new List<Employee>();
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public byte IsHeadquarter { get; set; }
        public byte Removed { get; set; }
    }
}
