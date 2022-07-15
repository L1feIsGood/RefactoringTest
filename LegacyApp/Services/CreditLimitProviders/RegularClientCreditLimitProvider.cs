namespace LegacyApp.Services
{
    public class RegularClientCreditLimitProvider : BaseCreditLimitProvider, ICreditLimitProvider
    {
        public RegularClientCreditLimitProvider(IUserCreditService userCreditService) : base(userCreditService)
        {
        }
        
        public override int? GetCreditLimit(IUser user)
        {
            return UserCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
        }

        public override bool HasCreditLimit()
        {
            return true;
        }
    }
}