using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using STP_Task.Models;

namespace STP_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService aService;
        public HomeController(IAccountService accountService)
        {
            this.aService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ViewLogin()
        {
            var vm = new LoginViewModel();
            return PartialView("_Login", vm);
        }

        public IActionResult ViewRegister()
        {
            var vm = new RegisterViewModel();
            return PartialView("_Register", vm);
        }

        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!this.ModelState.IsValid)
                return View("Login", vm);

            try
            {
                var user = await this.aService.AttemptUserLoginAsync(vm.Username, vm.Password);
                SignInUser(user);
            }
            catch (Exception ex) { }
            return ReturnHome();
        }
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!this.ModelState.IsValid)
                return View("_Register", vm);
            try
            {
                if (vm.Password == vm.ConfirmP && vm.Password.Length >= 6)
                {
                    await this.aService.AddAccountAsync(vm.Username, vm.Password);
                    var user = await aService.AttemptUserLoginAsync(vm.Username, vm.Password);
                    SignInUser(user);
                }
                else
                {
                    return View("_Register", vm);
                }
                return ReturnHome();
            }
            catch (Exception)
            {
                return ReturnHome();
            }
        }
        public async Task<IActionResult> CheckIfUsernameAvailable(string username)
        {
            try
            {
                var user = await aService.FindUserByUserNameAsync(username);
            }
            catch (Exception)
            {
                return Json("available");
            }
            return Json("unavailable");
        }

        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return ReturnHome();
        }

        private async void SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24),
                IssuedUtc = DateTimeOffset.UtcNow,
                IsPersistent = true,
            };

            await this.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
        private RedirectToActionResult ReturnHome()
          => RedirectToAction("Index", "Home");

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
