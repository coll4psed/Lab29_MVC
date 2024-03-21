using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab28_MVC.ViewModels;
using Lab28_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab28_MVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private loginEditDatabaseContext db;
        public AccountController(loginEditDatabaseContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserInfo user = await db.UserInfos.FirstOrDefaultAsync(u => u.Nickname == model.Nickname &&
                u.UserPassword == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Nickname);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        public IActionResult Register()
        {
            using(loginEditDatabaseContext db = new loginEditDatabaseContext())
            {
                var questions = db.QuestionInfos.Select(e => e).ToList();
                ViewBag.question = new SelectList(questions, "IdQuestion", "QuestionValue");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserInfo user = await db.UserInfos.FirstOrDefaultAsync(u => u.Nickname == model.Nickname);
                if (user == null)
                {
                    db.UserInfos.Add(new UserInfo{ Nickname = model.Nickname, UserPassword = model.Password,
                    Phone = model.PhoneNumber, IdQuestion = model.QuestionId, Answer = model.Answer});
                    await db.SaveChangesAsync();

                    await Authenticate(model.Nickname);

                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie" ,ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await db.UserInfos.FirstOrDefaultAsync(e => e.Nickname == User.Identity.Name);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.SecondName = model.SecondName;
                    if (model.Birthday < DateTime.Now)
                        user.Birthday = model.Birthday;
                    else
                        user.Birthday = null;
                    user.Country = model.Country;
                    user.City = model.City;
                    user.IdGender = model.IdGender;

                    await db.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Что-то введенно неправильно");
            }
            return View(model);
        }
    }
}
