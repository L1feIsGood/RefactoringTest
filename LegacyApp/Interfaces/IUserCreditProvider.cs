namespace LegacyApp
{
    public interface IUserCreditProvider
    {
        string ClientType { get;}
        CreditModel GenerateClientModel();
    }
}
