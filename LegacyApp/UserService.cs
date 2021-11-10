using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            int age = CheckAge(dateOfBirth);

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && email.Contains("@") && email.Contains(".") && age >= 21)
            {
                var clientRepository = new ClientRepository();
                var client = clientRepository.GetById(clientId);
                var user = new User
                {
                    Client = client,
                    DateOfBirth = dateOfBirth,
                    EmailAddress = email,
                    FirstName = firstName,
                    Surname = lastName
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
                        creditLimit *= 2;
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

                if (user.HasCreditLimit && user.CreditLimit < 500) return false;

                UserDataAccess.AddUser(user);

                return true;
            }
            else return false;
        }

        /// <summary>
        /// Метод для расчета возраста
        /// </summary>
        /// <param name="dateOfBirth">Дата рождения</param>
        /// <returns>Возвращает возраст</returns>
        private int CheckAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > now.AddYears(-age)) age--;
            return age;
        }
    }
}