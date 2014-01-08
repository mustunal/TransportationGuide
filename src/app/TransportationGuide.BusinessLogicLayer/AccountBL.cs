using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.ViewModels.AccountViewModels;
using TransportationGuide.ViewModels.Models;
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
                                Id = _userModel.Id,
                                BirthDate = _userModel.BirthDate,
                                Email = _userModel.Email,
                                Name = _userModel.Name,
                                Password = _userModel.Password,
                                Surname = _userModel.Surname,
                                Username = _userModel.Username
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
