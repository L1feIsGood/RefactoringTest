using LegacyApp.Extensions;
using System;

namespace LegacyApp.Validation
{
    public static class ValidUserData
    {
        public static bool IsValidData(string firName, string surName, string email, DateTime dateOfBirth)
        {
            bool flag = true;
            if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surName))
                flag = false;

            if (!email.Contains("@") && !email.Contains("."))
                flag = false;

            if (dateOfBirth.GetAgeUser() < 21)
                flag = false;

            return flag;
        }
    }
}
