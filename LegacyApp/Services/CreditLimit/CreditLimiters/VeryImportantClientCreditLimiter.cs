using LegacyApp.Models;

namespace LegacyApp.Services.CreditLimit.CreditLimiters
{
    internal class VeryImportantClientCreditLimiter : ICreditLimiter
    {
        public bool HasCreditLimit { get; }

        public VeryImportantClientCreditLimiter()
        {
            HasCreditLimit = false;
        }
        public int GetCreditLimit(User user)
        {
            return 0;
        }
    }
}