using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;

        public UserService()
        {
            _clientRepository = new ClientRepository();
        }

        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (firstName == null || firstName.Length == 0 || surname == null || surname.Length == 0)
                return false;

            if (!email.Contains("@") && !email.Contains("."))
                return false;

            var dateTimeNow = DateTime.Now;
            int ageUser = dateTimeNow.Year - dateOfBirth.Year;
            if (dateTimeNow.Month < dateOfBirth.Month ||
                (dateTimeNow.Month == dateOfBirth.Month && dateTimeNow.Day < dateOfBirth.Day)) ageUser--;

            if (ageUser < 21)
                return false;

            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                Surname = surname
            };

            switch (client.Name)
            {
                case "VeryImportantClient":
                    // Пропустить проверку лимита
                    CheckLimit(false);
                    break;
                case "ImportantClient":
                    // Проверить лимит и удвоить его
                    CheckLimit(true, 2, user);
                    break;
                default:
                    // Проверить лимит
                    CheckLimit(true, 1, user);
                    break;
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
                return false;

            UserDataAccess.AddUser(user);

            return true;
        }

        /// <summary>
        /// Проверка лимита и умножение на указанную ставку
        /// </summary>
        /// <param name="hasCreditLimit">Есть кредитний лимит?</param>
        /// <param name="rateCreditlimit">Ставка кредитного лимита</param>
        /// <param name="user">Пользователь кредитного лимита</param>
        private void CheckLimit(bool hasCreditLimit, int rateCreditlimit = 1, User user = null)
        {
            if (hasCreditLimit)
            {
                user.HasCreditLimit = hasCreditLimit;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    creditLimit = creditLimit * rateCreditlimit;
                    user.CreditLimit = creditLimit;
                }
            }
        }
    }
}
