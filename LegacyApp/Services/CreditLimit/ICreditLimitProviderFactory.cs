namespace LegacyApp
{
    public interface ICreditLimitProviderFactory
    {
        ICreditLimitProvider GetCreditLimitProvider(string clientName);
    }
}