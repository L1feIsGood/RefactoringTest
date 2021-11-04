namespace LegacyApp.ClientTypes
{
    public class VeryImportantClient : IUserCreditProvider
    {
        const int rateLimit = 0;
        public string ClientType => "VeryImportantClient";

        public CreditModel GenerateClientModel()
        {
            return new CreditModel
            {
                HasCreditLimit = false,
                Creditlimit = rateLimit
            };
        }
    }
}
