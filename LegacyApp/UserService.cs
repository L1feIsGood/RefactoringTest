using LegacyApp.Enums;
using LegacyApp.Models;
using LegacyApp.Repository;
using LegacyApp.Services;
using LegacyApp.Services.Validators;
using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            UserInfoValidator userValidator = new UserInfoValidator();
            bool isUserValid = userValidator.Validate(firName, surname, email, dateOfBirth);
            if (!isUserValid)
            {
                return false;
            }

            ClientRepository clientRepository = new ClientRepository();
            Client client = clientRepository.GetById(clientId);
            if (client == null)
            {
                return false;
            }

            UserCreator userCreator = new UserCreator();
            User user = userCreator.Create(client, firName, surname, email, dateOfBirth);

            UserCreditLimitValidator userCreditLimitValidator = new UserCreditLimitValidator();
            bool isUserCreditLimitValid = userCreditLimitValidator.Validate(user.HasCreditLimit, user.CreditLimit);
            if (!isUserCreditLimitValid)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}