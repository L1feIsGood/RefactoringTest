using System;
using System.Text.RegularExpressions;

namespace LegacyApp
{
    public class UserService
    {
        public int ageMin = 21;
        public int сreditLimitMin = 500;
        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var isNameValid = !string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(surname);
            if (!isNameValid)
            {
                return false;
            }

            Regex regex = new Regex(@"^\S*\@\S*\.\w*$");
            var isEmailValid = regex.Match(email).Success;
            if (!isEmailValid)  
            {
                return false;
            }

            var isAgeValid = !(Math.Floor((DateTime.Now - dateOfBirth).TotalDays / 365) < ageMin);
            if (!isAgeValid)
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
                Surname = surname
            };

            switch (client.Name)
            {
                case "VeryImportantClient":
                    // Пропустить проверку лимита
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    // Проверить лимит и удвоить его
                    var userCreditService = new UserCreditServiceClient();
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    break;
                default:
                    // Проверить лимит
                    userCreditService = new UserCreditServiceClient();
                    creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                    user.HasCreditLimit = true;
                    break;
            }

            var isCreditLimitValid = user.HasCreditLimit && user.CreditLimit < сreditLimitMin;
            if (!isCreditLimitValid)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}