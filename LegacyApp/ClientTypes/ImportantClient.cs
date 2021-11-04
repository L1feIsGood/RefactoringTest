namespace LegacyApp.ClientTypes
{
    public class ImportantClient : IUserCreditProvider
    {
        const int rateLimit = 2;

        public string ClientType => "ImportantClient";

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
