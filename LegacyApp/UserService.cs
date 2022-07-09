using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            UserFactory user_factory = new UserFactory();

            DateTime now_time = DateTime.Now;
            int age_user = (int)((now_time - dateOfBirth).TotalDays / 365);            

            if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname) || age_user < 21 || !email.Contains("@") && !email.Contains("."))
            {
                return false;
            }            

            User new_user = user_factory.CreateUser(clientId, dateOfBirth, email, firName, surname);

            if (new_user.HasCreditLimit && new_user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(new_user);

            return true;
        }
    }

    
}