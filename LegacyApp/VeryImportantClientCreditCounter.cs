namespace LegacyApp
{
    public class VeryImportantClientCreditCounter : IClientCreditCounter
    {

        public VeryImportantClientCreditCounter()
        {
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