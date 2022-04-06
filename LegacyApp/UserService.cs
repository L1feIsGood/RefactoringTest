using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (!CheckName(firstName, surname) || !CheckEMail(email) || !CheckAge(dateOfBirth))
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
                FirstName = firstName,
                Surname = surname
            };

            SetCreditLimit(user);

            if (!CheckCreditLimit(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }


        #region Private methods

        private bool CheckCreditLimit(User user)
        {
            if(user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }

        private bool CheckName(string firstName, string surname)
        {
            if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            return true;
        }

        private bool CheckEMail(string email)
        {
            if(!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            return true;
        }

        private bool CheckAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if(now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            if(age < 21)
            {
                return false;
            }

            return true;
        }

        private void SetCreditLimit(User user)
        {
            if(user.Client.Name == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
                return;
            }

            var creditMultiplier = 1;
            if(user.Client.Name == "ImportantClient")
            {
                creditMultiplier = 2;
            }

            using(var userCreditService = new UserCreditServiceClient())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                user.CreditLimit = creditLimit * creditMultiplier;
            }
        }

        #endregion
    }
}