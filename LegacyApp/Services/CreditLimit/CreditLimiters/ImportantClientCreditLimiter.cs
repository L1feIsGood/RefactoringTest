using LegacyApp.Models;

namespace LegacyApp.Services.CreditLimit.CreditLimiters
{
    internal class ImportantClientCreditLimiter : ICreditLimiter
    {
        public bool HasCreditLimit { get; }

        public ImportantClientCreditLimiter()
        {
            HasCreditLimit = true;
        }
        public int GetCreditLimit(User user)
        {
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                creditLimit *= 2;
                return creditLimit;
            }
        }
    }
}