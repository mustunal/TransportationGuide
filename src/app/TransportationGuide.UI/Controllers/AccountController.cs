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
using TransportationGuide.BusinessLogicLayer.OperationResults;

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
                OperationResult result = UserBL.AddNewUser(model);

                if (result.Status == OperationResultStatus.Success)
                {
                    var loginModel = new LoginViewModel { Username = model.Username, Password = model.Password };
                    return Login(loginModel, null);
                }
                else
                    ViewBag.RegisterResult = result.Message;
            }
            return View(model);
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
                var user = AccountBL.Login(model);
                if (user != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim(ClaimTypes.Surname, user.Surname));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.Role, ""));

                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    var authManager = Request.GetOwinContext().Authentication;
                    authManager.SignIn(new AuthenticationProperties() { IsPersistent = model.RememberMe }, identity);

                    //Session Ayarla
                    HttpContext.Session["User"] = user;

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
        [Authorize]
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
    }
}