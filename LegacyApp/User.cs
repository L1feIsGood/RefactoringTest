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

        public int CreditLimit => CalculateCreditLimit();

        public bool HasCreditLimit => !Client.IsVeryImportantClient();

        public bool IsResponsible()
        {
            return IsAdult() && ((HasCreditLimit && CreditLimit >= 500) || Client.IsVeryImportantClient());
        }

        private bool IsAdult()
        {
            return CalculateAge() >= 21;
        }

        private int CalculateAge()
        {
            // Можно описать класс калькулятора возраста (по Single Responsibility Principle) и вынести в него этот метод!
            // и 365 дней в году вынести в константу
            var span = DateTime.Now.Subtract(DateOfBirth);
            return (int)span.TotalDays / 365;
        }

        private int CalculateCreditLimit()
        {
            if (!HasCreditLimit)
                return 0;

            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(FirstName, Surname, DateOfBirth);
                return Client.IsImportantClient() ? 2 * creditLimit : creditLimit;
            }  
        }
    }
}