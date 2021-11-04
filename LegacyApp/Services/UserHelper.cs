namespace LegacyApp
{
    using System;

    public class UserHelper : IUserHelper
    {
        public bool IsStringEmptyOrNull(string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            return false;
        }

        public bool IsEmailCorrect(string email)
        {
            if (email.Contains("@") && email.Contains("."))
                return true;
            return false;
        }

        public bool IsAgeCorrect(DateTime dateBirthday)
        {
            var dateTimeNow = DateTime.Now;
            var ageUser = dateTimeNow.Year - dateBirthday.Year;
            if (dateTimeNow.Month < dateBirthday.Month ||
                (dateTimeNow.Month == dateBirthday.Month && dateTimeNow.Day < dateBirthday.Day))
                ageUser--;

            if (ageUser < 21)
                return false;
            return true;
        }

        public bool IsCreditLimitCorrect(bool hasCreditLimit, int creditLimit)
        {
            if (hasCreditLimit && creditLimit < 500)
                return false;
            return true;
        }
    }
}
