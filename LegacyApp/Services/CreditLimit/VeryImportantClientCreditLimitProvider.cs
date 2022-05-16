using System;

namespace LegacyApp
{
    public class VeryImportantClientCreditLimitProvider : BaseCreditLimitProvider, ICreditLimitProvider
    {
        public VeryImportantClientCreditLimitProvider(IUserCreditService userCreditService) : base(userCreditService)
        {
        }

        public override int? GetCreditLimit(string firstName, string surname, DateTime dateOfBirth)
        {
            var creditLimit = UserCreditService.GetCreditLimit(firstName, surname, dateOfBirth);
            creditLimit *= 2;
            return creditLimit;
        }
    }
}