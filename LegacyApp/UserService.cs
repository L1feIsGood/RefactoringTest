using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly ClientRepository _clientRepository;
        private readonly Validator _validator;
        private readonly ClientRoleGenerate _clientRoleGenereate;
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _validator = new Validator();
            _clientRoleGenereate = new ClientRoleGenerate();
        }
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (!_validator.IsNameValid(firName, surname))
            {
                return false;
            }
            if (!_validator.IsEmailValid(email))
            {
                return false;
            }          
            if (!_validator.IsAgeValid(dateOfBirth))
            {
                return false;
            }

            var client = _clientRepository.GetById(clientId);
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };

            var clientRole = _clientRoleGenereate.CreateClientRole(client.Name);
            user.HasCreditLimit = clientRole.HasCreditLimit;
            if (user.HasCreditLimit)
            {
                user.CreditLimit = clientRole.CalculateCreditLimit(user);
                if (_validator.IsCreditLimitValid(user.CreditLimit))
                {
                    return false;
                }
            }            

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}