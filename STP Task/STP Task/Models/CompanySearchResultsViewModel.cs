using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP_Task.Models
{
    public class CompanySearchResultsViewModel
    {
        public CompanySearchResultsViewModel()
        {
            this.Companies = new List<CompanyViewModel>();
        }
        public bool Searched { get; set; }
        public int Page { get; set; }
        public string Keyword { get; set; }
        public bool LastPage { get; set; }
        public List<CompanyViewModel> Companies { get; set; }
    }
}
