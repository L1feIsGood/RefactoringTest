using LegacyApp.Services;
using System;

namespace LegacyApp.Models
{
    public class UserParameters : IUser
    {
        public Client Client { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int CreditLimit { get; set; }
        public bool HasCreditLimit { get; set; }

        public bool IsValid()
        {
            bool output = true;

            // валидатор
            UserValidator validator = new UserValidator();

            // валидация имени
            output &= validator.ValidateNamePart(FirstName);

            // валидация фамилии
            output &= validator.ValidateNamePart(Surname);

            // валидация email
            output &= validator.ValidateEmail(EmailAddress);

            // валидация возраста
            output &= validator.ValidateAge(DateOfBirth);

            return output;
        }
    }
}
