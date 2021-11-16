using System;

namespace LegacyApp
{
    public class User
    {
        public Client Client { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int CreditLimit { get; set; }
        public bool HasCreditLimit { get; set; }

        public User CreateUser(Client client, DateTime dateOfBirth, string email, string firName, string surname)
        {

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
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);

                    // Если важный клиент, удвоить лимит
                    if (client.Name == "ImportantClient")
                    {
                        creditLimit = creditLimit * 2;
                    }
                    
                    user.CreditLimit = creditLimit;

                }
            }

            return user;
        }
    }
}