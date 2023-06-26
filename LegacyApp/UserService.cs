using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string surName, string email, DateTime birthday, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surName))
            {
                return false;
            }

            /*
             * Нужно "или", т.к. в email должна быть и '@', и точка. Не меняю, т.к. бизнес логику не трогаем, 
             * но это вопрос к коллегам (а вдруг так было и нужно а это я тут что-то не пониманию)
             */
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var currentDate = DateTime.Now;
            int age = currentDate.Year - birthday.Year;

            if (currentDate.Month < birthday.Month ||
                (currentDate.Month == birthday.Month && currentDate.Day < birthday.Day))
                age--;

            if (age < 21)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = birthday,
                EmailAddress = email,
                FirstName = firstName,
                Surname = surName
            };

            if (client.Name == "VeryImportantClient")
            {
                // Пропустить проверку лимита
                user.HasCreditLimit = false;
            }
            else
            {
                using (var userCreditService = new UserCreditServiceClient())
                {
                    user.HasCreditLimit = true;
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);

                    if (client.Name == "ImportantClient")
                    {
                        creditLimit = creditLimit * 2;
                    }
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