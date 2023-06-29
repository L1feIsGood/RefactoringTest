using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (!ValidateFields(firName, surname, email))
                return false;

            if (!ValidateAge(dateOfBirth))
                return false;

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

            user.SetLimit();

            if (user.HasCreditLimit && user.CreditLimit < 500)
                return false;

            UserDataAccess.AddUser(user);

            return true;
        }

        private bool ValidateFields(string firName, string surname, string email)
        {
            if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname))
                return false;

            if (!email.Contains("@") && !email.Contains("."))
                return false;

            return true;
        }

        private bool ValidateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;

            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            if (age < 21)
                return false;

            return true;
        }
    }
}