﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.RepositoryLayer.UnitOfWorks;
using TransportationGuide.ViewModels.AccountViewModels;
using TransportationGuide.Entities;
using TransportationGuide.BusinessLogicLayer.OperationResults;

namespace TransportationGuide.BusinessLogicLayer
{
    public class UserBL
    {
        public static OperationResult AddNewUser(RegisterViewModel model)
        {
            OperationResult _result = null;
            using (var uow = new UnitOfWork())
            {
                try
                {
                    if (String.Equals(model.Password, model.PasswordAgain, StringComparison.InvariantCultureIgnoreCase))
                    {

                        var userNameAlreadyExist = uow.UserRepository.GetUserByUserName(model.Username);
                        if (userNameAlreadyExist == null)
                        {
                            User newUser = new User
                            {
                                Name = model.Name,
                                Surname = model.Surname,
                                Username = model.Username,
                                Password = model.Password,
                                Email = model.Email
                            };
                            uow.UserRepository.Add(newUser);

                            uow.Commit();
                            _result = new OperationResult(OperationResultStatus.Success, "Kayıt Yapıldı");
                        }
                        else
                            _result = new OperationResult(OperationResultStatus.Fail, "Kullanıcı Adı Zaten Var");
                    }
                    else
                        _result = new OperationResult(OperationResultStatus.Fail, "Şifreler Aynı Değil");
                }
                catch (Exception ex)
                {
                    _result = new OperationResult(OperationResultStatus.Error, "Hata");
                }
            }
            return _result;
        }
    }
}
