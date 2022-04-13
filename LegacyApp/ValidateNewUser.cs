using System;

namespace LegacyApp;

public static class ValidateNewUser
{
    public static bool Validate(
            string firstName,
            string surname,
            string email,
            DateTime dateOfBirth)
    {
        if (!CheckingUserName(firstName, surname)
            || !CheckingEmail(email)
            || GetAge(dateOfBirth) < 21)
        {
            return false;
        }

        return true;
    }

    private static bool CheckingUserName(string firstName, string surname)
        => !(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname));

    private static bool CheckingEmail(string email)
        => email.Contains("@") && email.Contains(".");

    private static int GetAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;

        if (now.Month < dateOfBirth.Month
            || (now.Month == dateOfBirth.Month
                && now.Day < dateOfBirth.Day)
            )
        {
            age--;
        }

        return age;
    }
}

