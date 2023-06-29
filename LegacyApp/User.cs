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

        public void CheckCreditLimit()
        {
            HasCreditLimit = true;
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(FirstName, Surname, DateOfBirth);
                CreditLimit = creditLimit;
            }
        }

        public void IncreaseLimit(int amount = 1)
        {
            HasCreditLimit = true;
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(FirstName, Surname, DateOfBirth);
                creditLimit = creditLimit * amount;
                CreditLimit = creditLimit;
            }
        }

        public void SetLimit()
        {
            if (Client.Name == "VeryImportantClient")
                // Пропустить проверку лимита
                HasCreditLimit = false;

            else if (Client.Name == "ImportantClient")
                // Проверить лимит и удвоить его
                IncreaseLimit(2);
            else
                // Проверить лимит
                CheckCreditLimit();
        }
    }
}