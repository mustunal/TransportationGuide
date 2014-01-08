using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationGuide.ViewModels.UserViewModels;

namespace TransportationGuide.UI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View(UserViewModel userModel)
        {
            if (userModel == null)
                return RedirectToAction("NotFound", "Home");
            else
            {
                var profileModel = new UserProfileViewModel();
                profileModel.User = userModel;
                return View(profileModel);
            }
        }

        public ActionResult LoadProfileMenu(UserViewModel userModel)
        {
            return PartialView("_ProfileMenu", userModel);
        }
    }
}