using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationGuide.BusinessLogicLayer;
using TransportationGuide.ViewModels.UserViewModels;
using TransportationGuide.BusinessLogicLayer.OperationResults;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace TransportationGuide.UI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //
        // GET: /Profile/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View(UserSessionData userModel)
        {
            if (userModel == null)
                return RedirectToAction("NotFound", "Home");
            else
            {
                var profileModel = new UserProfileViewModel();
                profileModel.User = UserBL.GetUserById(userModel.Id);
                return View(profileModel);
            }
        }

        public ActionResult LoadProfileMenu(UserSessionData userData)
        {
            return PartialView("_ProfileMenu", userData);
        }

        public ActionResult Update(int id, UserSessionData userData)
        {
            if (userData.Id == id)
            {
                var userModel = UserBL.GetUserById(id);
                UserProfileViewModel userProfileModel = new UserProfileViewModel { User = userModel };

                return View("Update", userProfileModel);
            }
            else
                return RedirectToAction("NotFound", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserProfileViewModel userProfile)
        {
            OperationResult result = UserBL.UpdateUserProfile(userProfile);
            if (result.Status == OperationResultStatus.Success)
                return RedirectToAction("View");
            else
                ViewBag.UpdateResult = result.Message;

            return View(userProfile);
        }
    }
}