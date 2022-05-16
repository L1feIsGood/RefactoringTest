using System;

namespace LegacyApp
{
    public class ImportantClientCreditLimitProvider : BaseCreditLimitProvider, ICreditLimitProvider
    {
        public ImportantClientCreditLimitProvider(IUserCreditService userCreditService) : base(userCreditService)
        {
        }

        public override int? GetCreditLimit(string firstName, string surname, DateTime dateOfBirth)
        {
            return UserCreditService.GetCreditLimit(firstName, surname, dateOfBirth);
        }
    }
}