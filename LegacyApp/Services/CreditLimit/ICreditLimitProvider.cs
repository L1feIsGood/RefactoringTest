using System;

namespace LegacyApp
{
    public interface ICreditLimitProvider
    {
        /// <returns>Credit limit or null if user doesn't have one</returns>
        int? GetCreditLimit(string firstName, string surname, DateTime dateOfBirth);
    }
}