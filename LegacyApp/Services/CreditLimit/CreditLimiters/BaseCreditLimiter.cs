using LegacyApp.Models;

namespace LegacyApp.Services.CreditLimit.CreditLimiters
{
    internal class BaseCreditLimiter : ICreditLimiter
    {
        public bool HasCreditLimit { get;}

        public BaseCreditLimiter()
        {
            HasCreditLimit = true;
        }
        public int GetCreditLimit(User user)
        {
            using (var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                return creditLimit;
            }
        }
    }
}