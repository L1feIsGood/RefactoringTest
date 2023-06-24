using System;

namespace LegacyApp
{
    public class UserService
    {
        private const int MINIMUM_AGE = 21;
        private const int MINIMUM_CREDIT_LIMIT = 500;

        private static IClientRepository _clientRepository;
        private static IUserCreditService _userCreditService;

        public UserService() {
            IUserFactory userFactory = new UserFactory();
            _clientRepository = userFactory.CreateClientRepository();
            _userCreditService = userFactory.CreateUserCreditService();
        }

        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname)) return false;

            if (!IsValidEmail(email)) return false;

            if (!IsMinimumAge(dateOfBirth)) return false;

            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                Surname = surname
            };

            CalculateCreditLimit(user);

            if (HasCreditLimitLessThanMinimum(user)) return false;

            UserDataAccess.AddUser(user);

            return true;
        }

        private static bool IsValidEmail(string email) 
        {
            return email.Contains("@") && email.Contains(".");
        }

        private static bool IsMinimumAge(DateTime dateOfBirth) 
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;

            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < MINIMUM_AGE) return false;

            return true;
        }

        private static bool HasCreditLimitLessThanMinimum(User user) 
        {
            return user.HasCreditLimit && user.CreditLimit < MINIMUM_CREDIT_LIMIT;
        }

        private static void CalculateCreditLimit(User user) {
            switch (user.Client.Type) 
            {
                case ClientType.VeryImportantClient:
                    CalculateCreditLimitForVeryImportantClient(user);
                    break;
                case ClientType.ImportantClient:
                    CalculateCreditLimitImportantForClient(user);
                    break;
                default:
                    CalculateCreditLimitForOtherClient(user);
                    break;
            }
        }

        private static void CalculateCreditLimitForVeryImportantClient(User user) {
            // Пропустить проверку лимита
            user.HasCreditLimit = false;
        }

        private static void CalculateCreditLimitImportantForClient(User user) {
            // Проверить лимит и удвоить его
            user.HasCreditLimit = true;
            using (_userCreditService)
            {
                var creditLimit = _userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);  
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
        }

        private static void CalculateCreditLimitForOtherClient(User user) {
            // Проверить лимит
            user.HasCreditLimit = true;
            using (_userCreditService)
            {
                var creditLimit = _userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);  
                user.CreditLimit = creditLimit;
            }
        }
    }
}