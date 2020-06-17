using Data.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP_Task.Models
{
    public class EmployeeEditViewModel
    {
        public EmployeeEditViewModel()
        {
            ExperienceLevels = new List<SelectListItem>()
            {
                new SelectListItem("Junior","1"),
                new SelectListItem("Mid","2"),
                new SelectListItem("Senior","3"),
            };
        }
        public EmployeeEditViewModel(IEmployee employee)
        {
            this.Id = employee.Id;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.Salary = employee.Salary;
            this.VacationDays = employee.VacationDays;
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
            ExperienceLevels = new List<SelectListItem>()
            {
                new SelectListItem("Junior","1"),
                new SelectListItem("Mid","2"),
                new SelectListItem("Senior","3"),
            };
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartingDate { get; set; }
        public string ExperienceLevel { get; set; }
        public float Salary { get; set; }
        public int VacationDays { get; set; }
        public List<SelectListItem> ExperienceLevels { get; set; }
    }
}
