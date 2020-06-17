using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using STP_Task.Models;

namespace STP_Task.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService eService;
        public EmployeeController(IEmployeeService eService)
        {
            this.eService = eService;
        }

        public IActionResult Index()
        {
            var vm = new EmployeeSearchViewModel();
            return View(vm);
        }

        public async Task<IActionResult> DisplaySearchResults(string keyword, string page)
        {
            var model = new EmployeeSearchResultsViewModel()
            {
                Keyword = keyword,
                Searched = true
            };

            if (model.Keyword == null)
            {
                model.Keyword = "";
            }

            model.Page = int.Parse(page);
            Tuple<IList<Employee>, bool> tuple;
            tuple = await eService.SearchEmployeeByNameAsync(model.Keyword, model.Page);

            IList<Employee> employees = tuple.Item1;
            foreach (var template in employees)
            {
                model.Employees.Add(new EmployeeViewModel(template));
            }
            model.LastPage = tuple.Item2;
            return PartialView("_SearchResults", model);
        }

        public async Task<IActionResult> GetEditEmployee(string submitButton)
        {
            var employee = await eService.GetEmployeeAsync(int.Parse(submitButton));
            var vm = new EmployeeEditViewModel(employee);
            return View("EditEmployee", vm);
        }
        public async Task<IActionResult> EditEmployee(EmployeeEditViewModel vm)
        {
            await eService.EditEmployeeAsync(vm.Id, vm.FirstName, vm.LastName, vm.Salary, vm.VacationDays, byte.Parse(vm.ExperienceLevel));
            var vm1 = new EmployeeSearchViewModel();
            return View("Index", vm1);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            var vm = new EmployeeViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel vm)
        {
            await eService.AddEmployeeAsync(vm.FirstName, vm.LastName, vm.StartingDate, vm.Salary, vm.VacationDays, byte.Parse(vm.ExperienceLevel));
            var vm1 = new EmployeeSearchViewModel();
            return View("Index", vm1);
        }

        public async Task<IActionResult> RemoveEmployee(string submitButton)
        {
            await eService.RemoveEmployeeAsync(int.Parse(submitButton));
            var vm1 = new EmployeeSearchViewModel();
            return View("Index", vm1);
        }

    }
}