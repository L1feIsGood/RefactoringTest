using System;

namespace LegacyApp
{
    interface IUserFactory
    {
        IUserCreditService CreateUserCreditService();
        IClientRepository CreateClientRepository();
    }
}