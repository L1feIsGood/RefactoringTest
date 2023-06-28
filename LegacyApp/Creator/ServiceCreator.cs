namespace LegacyApp
{
    public class ServiceCreator : IServiceCreator
    {
        private const int MinUserAge = 21;
        private const int MinCreditLimit = 500;

        public static IServiceCreator Creator { get; private set; }

        static ServiceCreator()
        {
            Creator = new ServiceCreator();
        }

        public IClientRepository CreateClientRepository()
        {
            return new ClientRepository();
        }

        public IUserCreditService CreateUserCreditService()
        {
            return new UserCreditServiceClient();
        }

        public IUserCreditLimitChecker CreateUserCreditLimitChecker()
        {
            return new UserCreditLimitChecker(MinCreditLimit);
        }

        public IUserParamsValidator CreateUserParamsValidator()
        {
            return new UserParamsValidator(MinUserAge);
        }
    }
}
