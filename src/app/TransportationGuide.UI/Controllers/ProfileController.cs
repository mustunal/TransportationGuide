using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationGuide.BusinessLogicLayer;
using TransportationGuide.ViewModels.UserViewModels;
using TransportationGuide.BusinessLogicLayer.OperationResults;

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

        public ActionResult Update(int id)
        {
            var userModel = UserBL.GetUserById(id);
            UserProfileViewModel userProfileModel = new UserProfileViewModel { User = userModel };

            return View("Update", userProfileModel);
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