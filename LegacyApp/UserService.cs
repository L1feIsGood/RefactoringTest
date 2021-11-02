using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            UserValidator validator = new UserValidator();
            
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            
            if (!validator.IsNameValid(firName,surname) || !validator.IsEmailValid(email) || !validator.IsAgeValid(age))
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };
            
            var factory = new ClientCreditCounterFactory();
            var clientCreditCounter = factory.CreateCreditLimitCounter(client.Name);
            var creditLimit = clientCreditCounter.GetCreditLimit(user);
            user.HasCreditLimit = creditLimit.HasCreditLimit;
            user.CreditLimit = creditLimit.CreditLimit;

            if (!validator.IsCreditLimitValid(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}