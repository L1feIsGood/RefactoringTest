using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Services.Validators
{
    internal class UserCreditLimitValidator
    {
        private const int MinCreditLimit = 500;
        public bool Validate(bool hasCreditLimit, int creditLimit)
        {
            if (hasCreditLimit && creditLimit < MinCreditLimit)
            {
                return false;
            }

            return true;
        }
    }
}
