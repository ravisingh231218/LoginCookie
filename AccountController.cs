using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using UserLogin.Data;
using UserLogin.Models;

namespace UserLogin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext context;

        public AccountController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(loginSignUp model)
        {
            if (ModelState.IsValid)
            {
                var data = context.Users.Where(e => e.Username == model.Username).SingleOrDefault();
                if (data != null)
                {
                    bool isValid = (data.Username == model.Username && data.Password == model.Password);
                    if (isValid)
                    {


                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.Username) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principle = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                        HttpContext.Session.SetString("Username", model.Username);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["errorMessage"] = "Invalid Password";
                        return View(model);
                    }
                }
                else
                {
                    TempData["errorMessage"] = "Username not found";
                    return View(model);
                }

            }
            else
            {
                return View(model);
            }
        }

        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(UserView model)
        {
            if (ModelState.IsValid)
            {
                var data = new UserView()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Username = model.Username, 
                    Password = model.Password

                };
                context.Users.Add(data);
                context.SaveChanges();
                TempData["successMessage"] = "you are eligibal to login";
                return RedirectToAction("Login");

            }
            return View(model);
        }

       

       
        public IActionResult LogOut() 
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login","Account");
        }
    }
}
