namespace LegacyApp
{
    public interface IServiceCreator
    {
        IClientRepository CreateClientRepository();
        IUserCreditService CreateUserCreditService();
        IUserCreditLimitChecker CreateUserCreditLimitChecker();
        IUserParamsValidator CreateUserParamsValidator();
    }
}
