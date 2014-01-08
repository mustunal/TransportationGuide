using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationGuide.BusinessLogicLayer;
using TransportationGuide.ViewModels.UserViewModels;
using System.Security.Claims;

namespace TransportationGuide.UI.CustomModelBinders
{
    public class UserModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var user = controllerContext.HttpContext.Session["User"] as UserViewModel;
            if (user == null)
            {
                var authManager = controllerContext.HttpContext.GetOwinContext().Authentication;
                if (authManager != null && authManager.User != null)
                {
                    int userId = String.IsNullOrEmpty(authManager.User.FindFirst(ClaimTypes.Sid).Value) ? default(int) : Convert.ToInt32(authManager.User.FindFirst(ClaimTypes.Sid).Value);
                    if (userId != default(int))
                        controllerContext.HttpContext.Session["User"] = user = UserBL.GetUserById(Convert.ToInt32(authManager.User.FindFirst(ClaimTypes.Sid).Value));
                }
            }
            return user;
        }
    }
}