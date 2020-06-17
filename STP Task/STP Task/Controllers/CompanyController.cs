using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using STP_Task.Models;

namespace STP_Task.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService cService;
        private readonly IEmployeeService eService;

        public CompanyController(ICompanyService companyService, IEmployeeService employeeService)
        {
            this.cService = companyService;
            this.eService = employeeService;
        }

        public IActionResult Index()
        {
            var vm = new CompanySearchViewModel();
            return View(vm);
        }

        public async Task<IActionResult> DisplaySearchResults(string keyword, string page)
        {
            var model = new CompanySearchResultsViewModel()
            {
                Keyword = keyword,
                Searched = true
            };

            if (model.Keyword == null)
            {
                model.Keyword = "";
            }

            model.Page = int.Parse(page);
            Tuple<IList<Company>, bool> tuple;
            tuple = await cService.SearchCompanyByNameAsync(model.Keyword, model.Page);

            IList<Company> companies = tuple.Item1;
            foreach (var template in companies)
            {
                model.Companies.Add(new CompanyViewModel(template));
            }
            model.LastPage = tuple.Item2;
            return PartialView("_SearchResults", model);
        }

        [HttpGet]
        public IActionResult AddCompany()
        {
            var vm = new CompanyViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(CompanyViewModel vm)
        {
            await cService.AddCompanyAsync(vm.Name, vm.CreationDate);
            var vm1 = new CompanySearchViewModel();
            return View("Index", vm1);
        }

        public async Task<IActionResult> AddOffice(CompanyEditViewModel vm)
        {
            await cService.AddOfficeAsync(vm.NewOfficeCountry, vm.NewOfficeCity, vm.NewOfficeStreet, vm.NewOfficeStreetNumber, vm.Id);
            var company = await cService.FindCompanyByIdAsync(vm.Id);
            var vm1 = new CompanyEditViewModel(company, company.Offices.ToList());
            return View("EditCompany", vm1);
        }

        public async Task<IActionResult> RemoveOffice(string submitButton)
        {
            var id = await cService.RemoveOfficeAsync(int.Parse(submitButton));
            var company = await cService.FindCompanyByIdAsync(id);
            var vm1 = new CompanyEditViewModel(company, company.Offices.ToList());
            return View("EditCompany", vm1);
        }

        public async Task<IActionResult> AssignHeadquarter(string submitButton)
        {
            var id = await cService.AssignHeadquarters(int.Parse(submitButton));
            var company = await cService.FindCompanyByIdAsync(id);
            var vm1 = new CompanyEditViewModel(company, company.Offices.ToList());
            return View("EditCompany", vm1);
        }

        public async Task<IActionResult> UnassignHeadquarter(string submitButton)
        {
            var id = await cService.UnassignHeadquarters(int.Parse(submitButton));
            var company = await cService.FindCompanyByIdAsync(id);
            var vm1 = new CompanyEditViewModel(company, company.Offices.ToList());
            return View("EditCompany", vm1);
        }

        public async Task<IActionResult> GetEditCompany(string submitButton)
        {
            var company = await cService.FindCompanyByIdAsync(int.Parse(submitButton));
            var vm = new CompanyEditViewModel(company, company.Offices.ToList());
            return View("EditCompany", vm);
        }

        public async Task<IActionResult> EditCompany(CompanyEditViewModel vm)
        {
            await cService.EditCompany(vm.Id, vm.Name);
            var company = await cService.FindCompanyByIdAsync(vm.Id);
            var vm1 = new CompanyEditViewModel(company, company.Offices.ToList());
            return View("EditCompany", vm1);
        }

        public async Task<IActionResult> RemoveCompany(string submitButton)
        {
            await cService.RemoveCompanyAsync(int.Parse(submitButton));
            var vm = new CompanySearchViewModel();
            return View("Index", vm);
        }

        public async Task<IActionResult> GetOfficeAssigning(string submitButton)
        {
            var office = await cService.GetOfficeAsync(int.Parse(submitButton));
            var vm = new OfficeViewModel(office);
            return View("OfficeAssigning", vm);
        }

        public async Task<IActionResult> GetEmployeesForOffice(string keyword)
        {
            var employees = await eService.GetOfficeEmployeesAsync(int.Parse(keyword));
            var vm = new OfficeAssigningViewModel(employees);
            return PartialView("_OfficeAssigning", vm);
        }
        [HttpPost]
        public async Task<IActionResult> AssignEmployee(string eId, string oId)
        {
            await eService.AssignEmployeeAsync(int.Parse(eId), int.Parse(oId));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UnassignEmployee(string eId, string oId)
        {
            await eService.UnassignEmployeeAsync(int.Parse(eId));
            return Ok();
        }
    }
}