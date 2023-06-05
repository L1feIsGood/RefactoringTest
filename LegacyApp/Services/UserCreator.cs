using LegacyApp.Enums;
using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Services
{
    internal class UserCreator
    {
        private const int ImportantClientCreditCoefficient = 2;
        public User Create(Client client, string firName, string surname, string email, DateTime dateOfBirth)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname,
            };

            if (ClientType.VeryImportantClient.ToString() == client.Name)
            {
                user.HasCreditLimit = false;
            }
            else
            {
                user.HasCreditLimit = true;

                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);

                    user.CreditLimit = creditLimit;
                }

                if (ClientType.ImportantClient.ToString() == client.Name && user.CreditLimit > 0)
                {
                    user.CreditLimit *= ImportantClientCreditCoefficient;
                }
            }

            return user;
        }
    }
}
