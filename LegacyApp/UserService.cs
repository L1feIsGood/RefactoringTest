using System;

namespace LegacyApp
{
    public class UserService : IUserService
    {
        private readonly IUserCreditService _userCreditService;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;
        private readonly ICreditLimitSetterFactory _creditLimitSetterFactory;

        public UserService(IUserCreditService userCreditService,
            IClientRepository clientRepository,
            IUserRepository userRepository,
            IUserValidator userValidator,
            ICreditLimitSetterFactory creditLimitSetterFactory)
        {
            _userCreditService = userCreditService;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _userValidator = userValidator;
            _creditLimitSetterFactory = creditLimitSetterFactory;
        }

        public UserService() : this(
            new UserCreditServiceClient(),
            new ClientRepository(),
            new SqlUserRepository(),
            new UserValidator(new DefaultDateTimeService()),
            new CreditLimitSetterFactory())
        {

        }

        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var user = new User
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };

            // Проверить пользовательские данные на валидность.
            if (_userValidator.IsValid(user) == false)
                return false;

            user.Client = _clientRepository.GetById(clientId);

            // Установить кредитный лимит пользователю.
            SetCreditLimit(user);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            _userRepository.AddUser(user);

            return true;
        }

        private void SetCreditLimit(User user)
        {
            // Изначально неясно, будет ли расширяться функционал для клиентов, но заранее лучше заготовить почву.
            // Благодаря такой стратегии можно без проблем добавлять новую логику установки кредитных лимитов без редактирования UserService.
            var creditSetter = _creditLimitSetterFactory.Create(user);

            creditSetter.SetCreditLimit(user, _userCreditService);
        }
    }
}