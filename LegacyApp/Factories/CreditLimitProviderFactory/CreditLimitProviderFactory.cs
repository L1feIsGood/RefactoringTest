using LegacyApp.Services;

namespace LegacyApp.Factories
{
    public class CreditLimitProviderFactory : ICreditLimitProviderFactory
    {
        private readonly IUserCreditService _userCreditService;

        public CreditLimitProviderFactory(IUserCreditService userCreditService)
        {
            _userCreditService = userCreditService;
        }

        public ICreditLimitProvider GetCreditLimitProvider(string clientName)
        {
            switch (clientName)
            {
                case "ImportantClient":
                    return new ImportantClientCreditLimitProvider(_userCreditService);

                case "VeryImportantClient":
                    return new VeryImportantClientCreditLimitProvider(_userCreditService);

                default:
                    return new RegularClientCreditLimitProvider(_userCreditService);
            }
        }
    }
}