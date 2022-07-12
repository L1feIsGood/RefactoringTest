using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            int age = DateTime.Now.Subtract(dateOfBirth).Days / 365;

            if (age < 21)
            {
                return false;
            }

            var client = ClientRepository.GetClientById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };

            var userCreditService = new UserCreditServiceClient();

            user.HasCreditLimit = true;
            switch (client.Name)
            {
                case "VeryImportantClient":
                    // Пропустить проверку лимита
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    // Проверить лимит и удвоить его 
                    user.CreditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth) * 2;
                    break;
                default:
                    // Проверить лимит
                    user.CreditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    break;
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            userCreditService.Dispose();

            return true;
        }
    }
}