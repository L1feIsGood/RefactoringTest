using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var now_time = DateTime.Now;
            int age_user = now_time.Year - dateOfBirth.Year;
            if (now_time.Month < dateOfBirth.Month || (now_time.Month == dateOfBirth.Month && now_time.Day < dateOfBirth.Day)) age_user--;

            if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname) || age_user < 21 || !email.Contains("@") && !email.Contains("."))
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
            
            switch (client.Name)
            {
                case "VeryImportantClient":
                {
                    // Пропустить проверку лимита
                    user.HasCreditLimit = false;
                }break;
                case "ImportantClient":
                {
                        // Проверить лимит и удвоить его
                    user.HasCreditLimit = true;
                    using (var userCreditService = new UserCreditServiceClient())
                    {
                        var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                        creditLimit *= 2;
                        user.CreditLimit = creditLimit;
                    }
                }break;
                default:
                {
                    // Проверить лимит
                    user.HasCreditLimit = true;
                    using (var userCreditService = new UserCreditServiceClient())
                    {
                        var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                        user.CreditLimit = creditLimit;
                    }
                }break;
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