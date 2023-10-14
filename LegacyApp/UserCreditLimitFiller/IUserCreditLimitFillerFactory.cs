namespace LegacyApp.UserCreditLimitFiller
{
    public interface IUserCreditLimitFillerFactory
    {
        UserCreditLimitFillerBase GetUserCreditLimitFiller(string clientName);
    }
}
