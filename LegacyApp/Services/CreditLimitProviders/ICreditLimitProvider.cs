namespace LegacyApp.Services
{
    public interface ICreditLimitProvider
    {
        int? GetCreditLimit(IUser user);

        bool HasCreditLimit();
    }
}