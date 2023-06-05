using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Services.Validators
{
    internal class UserInfoValidator
    {
        public bool Validate(string firName, string surname, string email, DateTime dateOfBirth)
        {
            var isNameValid = ValidateName(firName, surname);
            if (!isNameValid)
            {
                return false;
            }

            var isDateOfBirthValid = ValidateDateOfBirth(dateOfBirth);
            if (!isDateOfBirthValid)
            {
                return false;
            }

            var isEmailValid = ValidateEmail(email);
            if (!isEmailValid)
            {
                return false;
            }

            return true;
        }

        private bool ValidateName(string firName, string surname)
        {
            if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            return true;
        }

        private bool ValidateEmail(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            return true;
        }

        private bool ValidateDateOfBirth(DateTime dateOfBirth)
        {
            const int MinAge = 21;

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day) age--;

            if (age < MinAge)
            {
                return false;
            }

            return true;
        }
    }
}
