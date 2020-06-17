using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP_Task.Models
{
    public class EmployeeSearchResultsViewModel
    {
        public EmployeeSearchResultsViewModel()
        {
            this.Employees = new List<EmployeeViewModel>();
        }
        public bool Searched { get; set; }
        public int Page { get; set; }
        public string Keyword { get; set; }
        public bool LastPage { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
    }
}
