namespace LegacyApp.CreditLimitCheckerStrategies
{
    public class OtherClientStrategy : ICreditLimitStrategy
    {
        private readonly IServiceCreator _serviceCreator;

        public OtherClientStrategy()
        {
            _serviceCreator = ServiceCreator.Creator;
        }

        public void CheckCreditLimit(User user)
        {
            // Проверить лимит
            user.HasCreditLimit = true;
            using (var userCreditService = _serviceCreator.CreateUserCreditService())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }
    }
}
