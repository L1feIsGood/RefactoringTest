using System;
using System.Text.RegularExpressions;

namespace LegacyApp
{
    public class UserValidator : IUserValidator
    {
        private readonly IDateTimeService _dateTimeService;

        private readonly Regex _mailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public UserValidator(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        public bool IsValid(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.Surname))
                return false;

            if (_mailRegex.IsMatch(user.EmailAddress) == false)
                return false;

            var currentDate = _dateTimeService.Now.Date;
            var ageDelta = currentDate - user.DateOfBirth.Date;
            var age = (int)Math.Floor(ageDelta.TotalDays / 365);

            if (age < 21)
                return false;

            return true;
        }
    }
}
