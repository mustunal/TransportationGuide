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
            var userData = controllerContext.HttpContext.Session["User"] as UserSessionData;
            if (userData == null)
            {
                var authManager = controllerContext.HttpContext.GetOwinContext().Authentication;
                if (authManager != null && authManager.User != null)
                {
                    int userId = String.IsNullOrEmpty(authManager.User.FindFirst(ClaimTypes.Sid).Value) ? default(int) : Convert.ToInt32(authManager.User.FindFirst(ClaimTypes.Sid).Value);
                    if (userId != default(int))
                    {
                        UserViewModel userModel = UserBL.GetUserById(Convert.ToInt32(authManager.User.FindFirst(ClaimTypes.Sid).Value)); ;
                        controllerContext.HttpContext.Session["User"] = userData = new UserSessionData { Id = userModel.Id, Name = userModel.Name, Surname = userModel.Surname, Password = userModel.Password, Username = userModel.Username };
                    }
                }
            }
            return userData;
        }
    }
}