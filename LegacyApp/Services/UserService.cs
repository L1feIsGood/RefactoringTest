using LegacyApp.Enums;
using LegacyApp.Models;
using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string surName, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surName))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (dateOfBirth > now.AddYears(-age)) age--;

            if (age < 21)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                Surname = surName
            };


            user.HasCreditLimit = client.ClientType != ClientType.VeryImportantClient;
            if (user.HasCreditLimit)
            {
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    if (client.ClientType == ClientType.ImportantClient)
                    {
                        user.CreditLimit = creditLimit * 2;
                    }
                    else
                    {
                        user.CreditLimit = creditLimit;
                    }
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}