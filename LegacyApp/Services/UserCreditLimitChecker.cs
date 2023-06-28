using LegacyApp.CreditLimitCheckerStrategies;

namespace LegacyApp
{
    public class UserCreditLimitChecker : IUserCreditLimitChecker
    {
        private readonly int _minCreditLimit;

        private ICreditLimitStrategy CreditLimitStrategy { get; set; }

        public UserCreditLimitChecker(int minCreditLimit)
        {
            _minCreditLimit = minCreditLimit;
        }

        public bool CheckCreditLimit(User user)
        {
            SetCreditLimit(user);
            if (user.HasCreditLimit && user.CreditLimit < _minCreditLimit)
            {
                return false;
            }
            return true;
        }

        private void SetCreditLimit(User user)
        {
            var clientType = user.Client.Name;

            switch (clientType)
            {
                case "VeryImportantClient":
                    CreditLimitStrategy = new VeryImportantClientStrategy();
                    break;
                case "ImportantClient":
                    CreditLimitStrategy = new ImportantClientStrategy();
                    break;
                default:
                    CreditLimitStrategy = new OtherClientStrategy();
                    break;
            }
            StartStrategy(user);
        }

        private void StartStrategy(User user)
        {
            CreditLimitStrategy.CheckCreditLimit(user);
        }
    }
}
