// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   AccountController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Account.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using FlowerFest.Controllers;
    using ViewModels.Home;

    [Authorize]
    [Area("Account")]
    public class HomeController : BaseController<HomeController>
    {
        private readonly string _password;
        private readonly string _salt;
        private readonly string _username;

        public HomeController(IConfiguration config,
            ILogger<HomeController> logger)
            : base(logger)
        {
            _password = config["user:password"];
            _salt = config["user:salt"];
            _username = config["user:username"];
        }
        
        [Route("/login")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            var username = model.UserName;
            var password = model.Password;

            try
            {
                if (ModelState.IsValid && username.Equals(_username) &&
                    VerifyPassword(password))
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, _username));

                    var principle = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties {IsPersistent = model.RememberMe};
                    await HttpContext.SignInAsync(principle, properties);

                    return RedirectToAction("Index", "Home", new { area = "Dashboard"});
                }

                ModelState.AddModelError(string.Empty, "Username or password is invalid.");
                return View("login", model);
            }
            catch (Exception e)
            {
               return ServerError(e);
            }
        }

        [Route("/logout")]
        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        [NonAction]
        private bool VerifyPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password can not be null or empty.");
            }

            var salt = Encoding.UTF8.GetBytes(_salt);

            var hash = KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                1000,
                256 / 8
            );

            return BitConverter.ToString(hash).Replace("-", string.Empty).Equals(_password);
        }
    }
}