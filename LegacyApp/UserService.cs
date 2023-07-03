using LegacyApp.Extensions;
using LegacyApp.Validation;
using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (!ValidUserData.IsValidData(firName, surname, email, dateOfBirth))
                return false;

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };

            int creditLimit = user.GetUserCreditLimit();

            switch (client.Name)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;

                case "ImportantClient":
                    user.HasCreditLimit = true;
                    user.CreditLimit = creditLimit * 2;
                    break;

                default:
                    user.HasCreditLimit = true;
                    user.CreditLimit = creditLimit;
                    break;
            }

            if (!user.CheckUserCreditLimit())
                return false;

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}