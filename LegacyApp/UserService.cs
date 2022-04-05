using System;
using System.Text.RegularExpressions;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname))
            {
                return false;
            }
            Regex nameValidation = new Regex(@"^[a-zA-Zа-яА-Я]+$");
            if (!nameValidation.IsMatch(firstName))
            {
                return false;
            }

            if (!nameValidation.IsMatch(surname))
            {
                return false;
            }

            Regex emailValidation = new Regex(@"^(.+)@(.+)\.(.+)$");
            if (!emailValidation.IsMatch(email))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age--;
            }

            if (age < 21)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);
            if (client == null)
            {
                return false;
            }

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                Surname = surname
            };

            IUserCreditService userCreditService = null;
            if (client.Name == "VeryImportantClient")
            {
                // Пропустить проверку лимита
                user.HasCreditLimit = false;
            }
            else if (client.Name == "ImportantClient")
            {
                // Проверить лимит и удвоить его
                user.HasCreditLimit = true;
                userCreditService = new UserCreditServiceClientImportant();
            }
            else
            {
                // Проверить лимит
                user.HasCreditLimit = true;
                userCreditService = new UserCreditServiceClientRegular();
            }

            if (user.HasCreditLimit && userCreditService != null)
            {
                user.CreditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
            }
            userCreditService?.Dispose();

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}