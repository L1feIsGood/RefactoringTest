using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if(IsValidEnteredData(firstName, surname, email))
            {
                var client = FindClient(clientId);

                var user = new User
                {
                    Client = client,
                    DateOfBirth = dateOfBirth,
                    EmailAddress = email,
                    FirstName = firstName,
                    Surname = surname
                };

                if(user.IsResponsible()) 
                    return AddUser(user);
            }
            return false;
        }

        private bool AddUser(User user)
        {
            UserDataAccess.AddUser(user);
            return true;
        }

        private Client FindClient(int clientId)
        {
            var clientRepository = new ClientRepository();
            return clientRepository.GetById(clientId);
        }


        private bool IsValidEnteredData(string firstName, string surname, string email)
        {
            return IsValidName(firstName, surname) && IsValidEmail(email);
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsValidName(string firstName, string surname)
        {
            return !string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(surname);
        }



       
    }
}