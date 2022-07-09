using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    class UserFactory
    {
        public User CreateUser(int clientId, DateTime dateOfBirth, string email, string firstName,string surName)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            User user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                Surname = surName
            };

            switch (client.Name)
            {
                case "VeryImportantClient":
                    {
                        // Пропустить проверку лимита
                        user.HasCreditLimit = false;
                    }
                    break;
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
                    }
                    break;
                default:
                    {
                        // Проверить лимит
                        user.HasCreditLimit = true;
                        using (var userCreditService = new UserCreditServiceClient())
                        {
                            var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                            user.CreditLimit = creditLimit;
                        }
                    }
                    break;
            }

            return user;
        }

        public enum ClientImportant
        {
            VeryImportantClient,
            ImportantClient
        }

    }
}
