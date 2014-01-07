using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.RepositoryLayer.UnitOfWorks;
using TransportationGuide.ViewModels.AccountViewModels;
using TransportationGuide.Entities;

namespace TransportationGuide.BusinessLogicLayer
{
    public class UserBL
    {
        public static bool AddNewUser(RegisterViewModel model)
        {
            bool isAdded = false;
            using (var uow = new UnitOfWork())
            {
                try
                {
                    User newUser = new User
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        Username = model.Username,
                        Password = model.Password
                    };

                    uow.UserRepository.Add(newUser);

                    uow.Commit();
                    isAdded = true;
                }
                catch (Exception ex)
                {
                    isAdded = false;
                }
            }
            return isAdded;
        }
    }
}
