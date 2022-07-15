namespace LegacyApp.Services
{
    public class ImportantClientCreditLimitProvider : BaseCreditLimitProvider, ICreditLimitProvider
    {
        public ImportantClientCreditLimitProvider(IUserCreditService userCreditService) : base(userCreditService)
        {
        }

        public override int? GetCreditLimit(IUser user)
        {
            var creditLimit = UserCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);

            creditLimit *= 2;

            return creditLimit;
        }

        public override bool HasCreditLimit()
        {
            return true;
        }
    }
}