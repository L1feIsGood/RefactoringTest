namespace LegacyApp
{
    public interface IClientCreditCounter
    {
        string ClientType { get; set; }
        
        CreditLimitModel GetCreditLimit(User user);
    }
}