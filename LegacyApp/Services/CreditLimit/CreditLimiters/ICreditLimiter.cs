using LegacyApp.Models;

namespace LegacyApp.Services.CreditLimit.CreditLimiters
{
    internal interface ICreditLimiter
    {
        bool HasCreditLimit { get; }
        int GetCreditLimit(User user);
    }
}