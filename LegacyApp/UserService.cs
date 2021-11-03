using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly UserValidator _validator;
        private readonly ClientCreditCounterFactory _factory;
        private readonly ClientRepository _repository;

        public UserService()
        {
            _validator = new UserValidator();
            _factory = new ClientCreditCounterFactory();
            _repository = new ClientRepository();
        }
        
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            
            if (!_validator.IsNameValid(firName,surname) || !_validator.IsEmailValid(email) || !_validator.IsAgeValid(age))
            {
                return false;
            }

            var client = _repository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };
            
            var clientCreditCounter = _factory.CreateCreditLimitCounter(client.Name);
            var creditLimit = clientCreditCounter.GetCreditLimit(user);
            user.HasCreditLimit = creditLimit.HasCreditLimit;
            user.CreditLimit = creditLimit.CreditLimit;

            if (!_validator.IsCreditLimitValid(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}