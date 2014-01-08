using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using TransportationGuide.UI.Models;
using WebMatrix.WebData;
using TransportationGuide.ViewModels.AccountViewModels;
using TransportationGuide.BusinessLogicLayer;

namespace TransportationGuide.UI.Controllers
{
    public class AccountController : Controller
    {

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool userLoggedIn = true;
                if (userLoggedIn)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, "Murat"));
                    claims.Add(new Claim(ClaimTypes.Email, "m@m.com"));

                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    var authManager = Request.GetOwinContext().Authentication;
                    authManager.SignIn(new AuthenticationProperties() { IsPersistent = model.RememberMe }, identity);

                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.LoginResultMessage = "Hatalı Kullanıcı Adı/Sifre!";
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "*")]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Manage()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isNewUserAdded = UserBL.AddNewUser(model);
                if (isNewUserAdded)
                    return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}