using System;
using System.Text.RegularExpressions;

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

            Regex regex = new Regex(@"^\S*\@\S*\.\w*$"); // регулярное выражение  {текст}@{текст}.{текст}

            if (regex.Match(email).Success) // проверка не допускающая @. без иных символов
            {
                return false;
            }

            if (Math.Floor((DateTime.Now - dateOfBirth).TotalDays / 365) < 21) // проверка года рождения без доп переменных
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
                FirstName = firName,
                Surname = surname
            };

            if (client.Name == "VeryImportantClient") 
            {
                // Пропустить проверку лимита
                user.HasCreditLimit = false;
            }
            else if (client.Name == "ImportantClient")
            {
                // Проверить лимит и удвоить его
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                // Проверить лимит
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
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