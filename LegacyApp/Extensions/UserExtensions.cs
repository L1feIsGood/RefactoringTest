using LegacyApp.Models;

namespace LegacyApp.Extensions
{
    internal static class UserExtensions
    {
        public static void UpdateCreditLimit(this User user, ClientCreditLimitInfo clientCreditLimitInfo)
        {
            if (clientCreditLimitInfo == null)
            {
                return;
            }

            user.HasCreditLimit = clientCreditLimitInfo.HasCreditLimit;
            user.CreditLimit = (int)(user.CreditLimit * clientCreditLimitInfo.CreditLimitMultiplier);
        }
    }
}
