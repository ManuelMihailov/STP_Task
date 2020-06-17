using Data.Contracts;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP_Task.Models
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
        }
        public CompanyViewModel(ICompany company)
        {
            this.Id = company.Id;
            this.CreationDate = company.CreationDate;
            this.Name = company.Name;
        }
        public CompanyViewModel(ICompany company, IList<Office>offices)
        {
            this.Id = company.Id;
            this.CreationDate = company.CreationDate;
            this.Name = company.Name;
            this.Offices = offices.Where(p => p.Removed == 0).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public IList<Office> Offices { get; set; }
    }
}
