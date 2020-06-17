using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP_Task.Models
{
    public class EmployeeSearchViewModel
    {
        public EmployeeSearchViewModel()
        {
            this.EmployeeSearchResultsViewModel = new EmployeeSearchResultsViewModel()
            {
                Searched = false
            };
        }

        public EmployeeSearchResultsViewModel EmployeeSearchResultsViewModel { get; set; }

    }
}
