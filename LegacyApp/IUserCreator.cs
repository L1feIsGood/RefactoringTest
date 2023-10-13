using System;

namespace LegacyApp
{
    public interface IUserCreator
    {
        User CreateUserFromClient(Client client, string firstName, string surname, string email, DateTime dateOfBirth);
    }
}
