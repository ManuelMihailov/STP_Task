using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Employee: IEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartingDate { get; set; }
        public float Salary { get; set; }
        public int VacationDays { get; set; }
        public byte ExperienceLevel { get; set; }
        public int? OfficeId { get; set; }
        public virtual Office CurrentOffice { get; set; }
        public string CompanyName { get; set; }
        public byte Removed { get; set; }
    }
}
