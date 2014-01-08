using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationGuide.UI.CustomModelBinders;
using TransportationGuide.ViewModels.UserViewModels;

namespace TransportationGuide.UI
{
    public class ModelBinderConfig
    {
        public static void RegisterCustomModelBinders()
        {
            ModelBinders.Binders.Add(typeof(UserViewModel), new UserModelBinder());
        }
    }
}