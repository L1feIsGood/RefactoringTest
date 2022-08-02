using System;

namespace LegacyApp
{
    public class UserCreditLimitConfigurationService : IUserCreditLimitConfigurationService
    {
        public bool ConfigureCreditLimit(User user)
        {
            switch (user.Client.Name)
            {
                case "VeryImportantClient":
                    // Пропустить проверку лимита
                    user.HasCreditLimit = false;
                    break;

                case "ImportantClient":
                    // Проверить лимит и удвоить его
                    user.HasCreditLimit = true;
                    using (var userCreditService = new UserCreditServiceClient())
                    {
                        var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                        creditLimit = creditLimit * 2;
                        user.CreditLimit = creditLimit;
                    }
                    break;

                default:
                    // Проверить лимит
                    user.HasCreditLimit = true;
                    using (var userCreditService = new UserCreditServiceClient())
                    {
                        var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                        user.CreditLimit = creditLimit;
                    }
                    break;
            }

            const int minCreditLimit = 500;
            if (user.HasCreditLimit && user.CreditLimit < minCreditLimit)
            {
                return false;
            }

            return true;
        }
    }
}
