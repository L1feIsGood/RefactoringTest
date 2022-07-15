using LegacyApp.Services;

namespace LegacyApp.Factories
{
    public interface ICreditLimitProviderFactory
    {
        ICreditLimitProvider GetCreditLimitProvider(string clientName);
    }
}