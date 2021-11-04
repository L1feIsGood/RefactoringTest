namespace LegacyApp.ClientTypes
{
    public class DefaultClient : IUserCreditProvider
    {
        const int rateLimit = 1;
        public string ClientType => null;

        public CreditModel GenerateClientModel()
        {
            return new CreditModel
            {
                HasCreditLimit = true,
                Creditlimit = rateLimit
            };
        }
    }
}
