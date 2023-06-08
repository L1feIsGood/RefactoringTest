using System;
using System.Text.RegularExpressions;

namespace LegacyApp
{
    public class UserService
    {
        public const int MinAllowedUserAge = 21;
        public const int MinAllowedCreditLimit = 500;
        public const int ImportantClientCreditLimitMultiplier = 2;

        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(email))
                return false;

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return false;

            var minAllowedDateOfBirth = DateTime.Now.AddYears(-MinAllowedUserAge);
            if (dateOfBirth < minAllowedDateOfBirth)
                return false;

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

            if (client.Name == Client.VeryImportantClientName)
            {
                // Пропустить проверку лимита
                user.HasCreditLimit = false;
                UserDataAccess.AddUser(user);
                return true;
            }

            // Проверить лимит
            user.HasCreditLimit = true;
            using (var userCreditService = new UserCreditServiceClient())
                user.CreditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
            // Удвоить лимит, если важный клиент
            if (client.Name == Client.ImportantClientName)
                user.CreditLimit *= ImportantClientCreditLimitMultiplier;
            if (user.CreditLimit < MinAllowedCreditLimit)
                return false;

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}