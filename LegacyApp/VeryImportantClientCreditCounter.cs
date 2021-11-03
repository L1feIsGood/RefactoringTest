namespace LegacyApp
{
    public class VeryImportantClientCreditCounter : IClientCreditCounter
    {
        public string ClientType { get; set; }

        public VeryImportantClientCreditCounter()
        {
            ClientType = "VeryImportantClient";
        }


        public CreditLimitModel GetCreditLimit(User user)
        {
            return new CreditLimitModel
            {
                HasCreditLimit = false,
                CreditLimit = 0
            };
        }
    }
}