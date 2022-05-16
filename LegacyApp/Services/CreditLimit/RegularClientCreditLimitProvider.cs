using System;

namespace LegacyApp
{
    public class RegularClientCreditLimitProvider : BaseCreditLimitProvider, ICreditLimitProvider
    {
        public RegularClientCreditLimitProvider(IUserCreditService userCreditService) : base(userCreditService)
        {
        }

        public override int? GetCreditLimit(string firstName, string surname, DateTime dateOfBirth)
        {
            return null;
        }
    }
}