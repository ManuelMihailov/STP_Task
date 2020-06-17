using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP_Task.Models
{
    public class OfficeAssigningViewModel
    {
        public OfficeAssigningViewModel(IList<Employee> employees)
        {
            this.Employees = new List<EmployeeViewModel>();
            foreach (var item in employees)
            {
                this.Employees.Add(new EmployeeViewModel(item));
            }
        }

        public List<EmployeeViewModel> Employees;
    }
}
