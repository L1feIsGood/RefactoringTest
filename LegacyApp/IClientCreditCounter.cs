namespace LegacyApp
{
    public interface IClientCreditCounter
    {
        CreditLimitModel GetCreditLimit(User user);
    }
}