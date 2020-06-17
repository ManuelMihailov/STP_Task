using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP_Task.Models
{
    public class OfficeViewModel
    {
        public OfficeViewModel()
        {
        }
        public OfficeViewModel(IOffice office)
        {
            this.Id = office.Id;
            this.Country = office.Country;
            this.City = office.City;
            this.StreetName = office.Street;
            this.CompanyName = office.Company.Name;
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public string CompanyName { get; set; }
    }
}
