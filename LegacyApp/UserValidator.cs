using System;

namespace LegacyApp
{
    public class UserValidator : IUserValidator
    {
        private const int minimumAge = 21;

        public bool IsValid(string firstName, string surname, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.UtcNow;
            int age = now.Year - dateOfBirth.Year;
            if (now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age--;
            }

            if (age < minimumAge)
            {
                return false;
            }
            return true;
        }
    }
}
