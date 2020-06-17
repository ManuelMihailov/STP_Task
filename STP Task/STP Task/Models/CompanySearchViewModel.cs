using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP_Task.Models
{
    public class CompanySearchViewModel
    {
        public CompanySearchViewModel()
        {
            this.CompanySearchResultsViewModel = new CompanySearchResultsViewModel()
            {
                Searched = false
            };
        }

        public CompanySearchResultsViewModel CompanySearchResultsViewModel { get; set; }

    }
}
