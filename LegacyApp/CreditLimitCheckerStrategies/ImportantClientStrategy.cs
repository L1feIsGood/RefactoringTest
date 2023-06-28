namespace LegacyApp.CreditLimitCheckerStrategies
{
    public class ImportantClientStrategy : ICreditLimitStrategy
    {
        private readonly IServiceCreator _serviceCreator;

        public ImportantClientStrategy()
        {
            _serviceCreator = ServiceCreator.Creator;
        }

        public void CheckCreditLimit(User user)
        {
            // Проверить лимит и удвоить его
            user.HasCreditLimit = true;
            using (var userCreditService = _serviceCreator.CreateUserCreditService())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
        }
    }
}
