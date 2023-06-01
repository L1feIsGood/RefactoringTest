using System;

namespace LegacyApp
{
    public interface IUserFactory
    {
        User Create(string firstName, string surname, string email, DateTime dateOfBirth, int clientId);
    }
}