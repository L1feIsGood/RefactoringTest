using System;

namespace LegacyApp.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetAgeUser(this DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            return age;
        }
    }
}

