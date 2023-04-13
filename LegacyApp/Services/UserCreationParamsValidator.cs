using System;

namespace LegacyApp.Services
{
    internal class UserCreationParamsValidator
    {
        public bool ValidateParams(UserCreationParams userCreationParams)
        {
            var isNameCorrect = ValidateName(userCreationParams.FirstName, userCreationParams.Surname);
            if (!isNameCorrect)
                return false;

            var isEmailValid = ValidateEmail(userCreationParams.Email);
            if (!isEmailValid)
                return false;

            var isAgeValid = ValidateAge(userCreationParams.DateOfBirth);
            if (!isAgeValid)
                return false;

            return true;
        }

        private bool ValidateName(string firstName, string surname)
        {
            var isFirstNameEmpty = string.IsNullOrEmpty(firstName);
            if (isFirstNameEmpty)
                return false;

            var isSurnameEmpty = string.IsNullOrEmpty(surname);
            if (isSurnameEmpty)
                return false;

            return true;
        }

        private bool ValidateEmail(string email)
        {
            var emailContainsSpecialSymbols = email.Contains("@") && email.Contains(".");

            return emailContainsSpecialSymbols;
        }

        private bool ValidateAge(DateTime dateOfBirth)
        {
            const int minimalAge = 21;

            var now = DateTime.Now;

            var age = now.Year - dateOfBirth.Year;
            if (dateOfBirth > now.AddYears(-age)) 
                age--;

            var isAgeBelowMinimalAge = age < minimalAge;
            return !isAgeBelowMinimalAge;
        }
    }
}