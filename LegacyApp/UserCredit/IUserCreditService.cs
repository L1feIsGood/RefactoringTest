using LegacyApp.Models;
using System;

namespace LegacyApp.UserCredit
{
    public interface IUserCreditService : IDisposable
    {
        int GetUserCreditLimit(User user);
        ClientCreditLimitInfo GetClientCreditLimitInfo(Client client);
    }
}
