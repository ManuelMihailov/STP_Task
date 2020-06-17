using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP_Task.Models
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {

        }
        public EmployeeViewModel(IEmployee employee)
        {
            this.Id = employee.Id;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.StartingDate = employee.StartingDate;
            this.Salary = employee.Salary;
            this.VacationDays = employee.VacationDays;
            this.CompanyName = employee.CompanyName;
            switch (employee.ExperienceLevel)
            {
                case 1:
                    ExperienceLevel = "Junior";
                    break;
                case 2:
                    ExperienceLevel = "Mid";
                    break;
                case 3:
                    ExperienceLevel = "Senior";
                    break;
                default:
                    break;
            }
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartingDate { get; set; }
        public string ExperienceLevel { get; set; }
        public float Salary { get; set; }
        public int VacationDays { get; set; }
        public string CompanyName { get; set; }

    }
}
