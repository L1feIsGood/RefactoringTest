namespace LegacyApp
{
    public class UserFactory : IUserFactory
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICreditLimitProviderFactory _creditLimitProviderFactory;

        public UserFactory(IClientRepository clientRepository,
            ICreditLimitProviderFactory creditLimitProviderFactory)
        {
            _clientRepository = clientRepository;
            _creditLimitProviderFactory = creditLimitProviderFactory;
        }

        public User Create(CreateUserParams createUserParams)
        {
            const int minCreditLimit = 500;
            
            if (!createUserParams.IsValid())
                return null;

            var client = _clientRepository.GetById(createUserParams.ClientId);
            if (client == null)
                return null;

            var creditLimitProvider = _creditLimitProviderFactory.GetCreditLimitProvider(client.Name);
            var creditLimit = creditLimitProvider.GetCreditLimit(createUserParams.FirstName, createUserParams.Surname,
                createUserParams.DateOfBirth);

            var isCreditLimitTooLow = creditLimit.HasValue && creditLimit < minCreditLimit; 
            if (isCreditLimitTooLow)
                return null;

            var user = new User
            {
                Client = client,
                DateOfBirth = createUserParams.DateOfBirth,
                EmailAddress = createUserParams.Email,
                FirstName = createUserParams.FirstName,
                Surname = createUserParams.Surname,
                CreditLimit = creditLimit ?? 0,
                HasCreditLimit = true
            };

            return user;
        }
    }
}