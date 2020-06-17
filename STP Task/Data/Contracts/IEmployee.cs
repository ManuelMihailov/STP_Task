using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contracts
{
    public interface IEmployee
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime StartingDate { get; set; }
        float Salary { get; set; }
        int VacationDays { get; set; }
        byte ExperienceLevel { get; set; }
        int? OfficeId { get; set; }
        Office CurrentOffice { get; set; }
        byte Removed { get; set; }
        string CompanyName { get; set; }
    }
}
