using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Company : ICompany
    {
        public Company()
        {
            Offices = new List<Office>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public byte Removed { get; set; }
        public ICollection<Office> Offices { get; set; }
    }
}
