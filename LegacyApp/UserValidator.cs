using System;

namespace LegacyApp
{
    public class UserValidator
    {
        public bool IsUserDataValid(string firName, string surname, string email, DateTime dateOfBirth)
        {
            return IsNameValid(firName,surname) && IsEmailValid(email) && IsDateOfBirthValid(dateOfBirth);
        }

        private bool IsNameValid(string firName, string surname)
        {
            return !string.IsNullOrEmpty(firName) && !string.IsNullOrEmpty(surname);
        }

        private bool IsEmailValid(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsDateOfBirthValid(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            return age >= 21;
        }

        public bool IsUserCreditLimitValid(User user)
        {
            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }
    }
}