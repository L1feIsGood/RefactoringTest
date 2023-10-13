using System;

namespace LegacyApp
{
    public class UserValidator
    {
        public bool Validate(string firstName, string surname, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age--;
            }

            if (age < 21)
            {
                return false;
            }
            return true;
        }
    }
}
