using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace IBS.Controllers
{
    public class AccountController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        [Route("login")]
        public ActionResult Login(string ReturnUrl = "")
        {
            ViewBag.Message = "";
            ViewBag.ReturnUrl = ReturnUrl;

            return View();

        }
        [Route("Register")]
        public ActionResult Register()
        {


            return View();

        }

        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User oUser = db.Users.Include(u => u.Role).FirstOrDefault(a => a.CellNum == model.Email && a.IsDeleted == false);
            
                if (oUser != null)
                {
                    TempData["duplicateUser"] =
                        "Your email has already been registered on our website, please login with this email or register with new email";
                    return View(model);
                }

                oUser = new User()
                {
                    Id=Guid.NewGuid(),
                    FullName = model.FullName,
                    Email = model.Email,
                    CellNum = model.Email,
                    Code = 0,
                    CreationDate = DateTime.Now,
                    Password = model.Password,
                    IsDeleted = false,
                    IsActive = true,
                    RoleId = new Guid("5ae6bff3-1dce-43a5-bc27-7f2f47ac4fde"),
                    Gender = model.Gender

                };
                db.Users.Add(oUser);
                db.SaveChanges();


                TempData["success"] =
                    "Your account is active now and you can login to site";
                return View(model);
            }


            return View(model);
        }



        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User oUser = db.Users.Include(u => u.Role).FirstOrDefault(a => a.CellNum == model.Username && a.Password == model.Password);

                if (oUser != null)
                {
                    var ident = new ClaimsIdentity(
                      new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
              new Claim(ClaimTypes.NameIdentifier, oUser.CellNum),
              new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
              new Claim(ClaimTypes.Name,oUser.Id.ToString()),

              // optionally you could add roles if any
               new Claim(ClaimTypes.Role, oUser.Role.Name),

                      },
                      DefaultAuthenticationTypes.ApplicationCookie);

                    HttpContext.GetOwinContext().Authentication.SignIn(
                       new AuthenticationProperties { IsPersistent = true }, ident);
                    return RedirectToLocal(returnUrl, oUser.Role.Name); // auth succeed 
                }
                else
                {
                    // invalid username or password
                    TempData["WrongPass"] = "Invalid email or password.";
                }
            }


            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl, string role)
        {

            if (role.ToLower() == "administrator")
                return RedirectToAction("index", "Users");

          
            return RedirectToAction("list", "Questions");

        }
        public ActionResult LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
            }
            return RedirectToAction("login", "Account");
        }
    }
}