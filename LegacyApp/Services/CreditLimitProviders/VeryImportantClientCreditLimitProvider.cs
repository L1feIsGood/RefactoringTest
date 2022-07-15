namespace LegacyApp.Services
{
    public class VeryImportantClientCreditLimitProvider : BaseCreditLimitProvider
    {
        public VeryImportantClientCreditLimitProvider(IUserCreditService userCreditService) : base(userCreditService)
        {
        }
        

        public override int? GetCreditLimit(IUser user)
        {
            return null;
        }

        public override bool HasCreditLimit()
        {
            return false;
        }
    }
}