using System;
using LegacyApp.Models;

namespace LegacyApp.UserValidation
{
    internal class DefaultCreditUserValidationRule : IUserValidationRule
    {
        private int _minCreditLimit;

        public DefaultCreditUserValidationRule(int minCreditLimit)
        {
            if (minCreditLimit < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minCreditLimit));
            }

            _minCreditLimit = minCreditLimit;
        }

        public bool IsUserDataValid(User user)
        {
            bool hasNoCreditLimit = !user.HasCreditLimit;
            bool hasMoreThanMinimum = user.HasCreditLimit && user.CreditLimit > _minCreditLimit;

            return hasNoCreditLimit || hasMoreThanMinimum;
        }
    }
}
