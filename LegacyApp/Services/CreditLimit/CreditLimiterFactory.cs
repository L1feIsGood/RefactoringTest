using System;
using System.Collections.Generic;
using LegacyApp.Services.CreditLimit.CreditLimiters;

namespace LegacyApp.Services.CreditLimit
{
    internal class CreditLimiterFactory
    {
        private readonly Dictionary<string, Type> _userNameWithCreditLimiterTypes;

        public CreditLimiterFactory()
        {
            _userNameWithCreditLimiterTypes = new Dictionary<string, Type>
            {
                {"VeryImportantClient",typeof(VeryImportantClientCreditLimiter)},
                {"ImportantClient",typeof(ImportantClientCreditLimiter)}
            };
        }


        public ICreditLimiter CreateCreditLimiter(string userName)
        {
            var isCreditLimiterExists =
                _userNameWithCreditLimiterTypes.TryGetValue(userName, out var creditLimiterType);

            if (isCreditLimiterExists)
            {
                var creditLimiter = (ICreditLimiter)Activator.CreateInstance(creditLimiterType);
                return creditLimiter;
            }

            var baseCreditLimiter = new BaseCreditLimiter();

            return baseCreditLimiter;
        }
    }
}