namespace LegacyApp.CreditLimitCheckerStrategies
{
    public class VeryImportantClientStrategy : ICreditLimitStrategy
    {
        public void CheckCreditLimit(User user)
        {
            // Пропустить проверку лимита
            user.HasCreditLimit = false;
        }
    }
}
