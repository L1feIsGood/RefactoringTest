using LegacyApp.Extensions;
using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidInput(firName, surname, email, dateOfBirth))
                return false;

            var client = GetClientById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };

            var creditLimit = GetUserCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);

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

            if (user.HasCreditLimit && user.CreditLimit < 500)
                return false;

            UserDataAccess.AddUser(user);

            return true;
        }

        private Client GetClientById(int clientId)
        {
            var clientRepository = new ClientRepository();
            return clientRepository.GetById(clientId);
        }

        private int GetUserCreditLimit(string firstName, string surname, DateTime dateOfBirth)
        {
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(firstName, surname, dateOfBirth);
                return creditLimit;
            }
        }

        private bool IsValidInput(string firName, string surname, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(surname))
                return false;

            if (string.IsNullOrEmpty(firName))
                return false;

            if (!email.Contains("@") && !email.Contains("."))
                return false;

            var age = dateOfBirth.GetTodayAge();
            if (age < 21)
                return false;

            return true;
        }
    }
}