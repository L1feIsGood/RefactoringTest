using System;

namespace LegacyApp
{
    public abstract class BaseCreditLimitProvider : ICreditLimitProvider
    {
        private protected readonly IUserCreditService UserCreditService;

        protected BaseCreditLimitProvider(IUserCreditService userCreditService)
        {
            UserCreditService = userCreditService;
        }

        public abstract int? GetCreditLimit(string firstName, string surname, DateTime dateOfBirth);
    }
}