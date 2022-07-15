namespace LegacyApp.Services
{
    public abstract class BaseCreditLimitProvider : ICreditLimitProvider
    {
        private protected readonly IUserCreditService UserCreditService;

        protected BaseCreditLimitProvider(IUserCreditService userCreditService)
        {
            UserCreditService = userCreditService;
        }

        public abstract int? GetCreditLimit(IUser user);
        public abstract bool HasCreditLimit();
    }
}