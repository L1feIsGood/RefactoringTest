using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserHelper _userHelper;


        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userHelper = new UserHelper();
        }

        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (_userHelper.IsStringEmptyOrNull(firName)
                || _userHelper.IsStringEmptyOrNull(surname)
                || !_userHelper.IsEmailCorrect(email)
                || !_userHelper.IsAgeCorrect(dateOfBirth))
                return false;

            var client = _clientRepository.GetById(clientId);
            User user;
            using (var userCreditService = new UserCreditServiceClient())
            {
                var userCredit = userCreditService.GetByType(client.Name);

                user = new User
                {
                    Client = client,
                    DateOfBirth = dateOfBirth,
                    EmailAddress = email,
                    FirstName = firName,
                    Surname = surname,
                    HasCreditLimit = userCredit.HasCreditLimit,
                    CreditLimit = userCredit.Creditlimit
                };

                user.CreditLimit = userCreditService.CalculateCreditLimit(user);
            }

            if (!_userHelper.IsCreditLimitCorrect(user.HasCreditLimit, user.CreditLimit))
                return false;

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}
