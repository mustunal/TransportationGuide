using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.ViewModels.AccountViewModels;
using TransportationGuide.ViewModels.UserViewModels;
using TransportationGuide.RepositoryLayer.UnitOfWorks;

namespace TransportationGuide.BusinessLogicLayer
{
    public class AccountBL
    {
        public static UserViewModel Login(LoginViewModel loginModel)
        {
            UserViewModel _userModel = null;
            using (var uow = new UnitOfWork())
            {
                try
                {
                    var user = uow.UserRepository.GetUserByUserName(loginModel.Username);
                    if (user != null)
                    {
                        if (user.Password.Equals(loginModel.Password, StringComparison.InvariantCultureIgnoreCase))
                            _userModel = new UserViewModel
                            {
                                Id = user.Id,
                                BirthDate = user.BirthDate,
                                Email = user.Email,
                                Name = user.Name,
                                Password = user.Password,
                                Surname = user.Surname,
                                Username = user.Username
                            };
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return _userModel;
        }
    }
}
