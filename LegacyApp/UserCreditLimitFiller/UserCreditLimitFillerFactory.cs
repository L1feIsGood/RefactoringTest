namespace LegacyApp.UserCreditLimitFiller
{
    public class UserCreditLimitFillerFactory : IUserCreditLimitFillerFactory
    {
        public UserCreditLimitFillerBase GetUserCreditLimitFiller(string clientName)
        {
            switch (clientName)
            {
                case SpecialClientNames.VeryImportantClient:
                    return new VeryImportantClientCreditLimitFiller();
                case SpecialClientNames.ImportantClient:
                    return new ImportantClientCreditLimitFiller();
                default:
                    return new DefaultUserCreditLimitFiller();
            }
        }
    }
}
