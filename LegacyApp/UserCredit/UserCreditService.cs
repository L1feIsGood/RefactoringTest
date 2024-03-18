using LegacyApp.Models;
using System.Linq;

namespace LegacyApp.UserCredit
{
    public class UserCreditService : IUserCreditService
    {
        private ClientCreditLimitInfo[] _clientCreditLimitInfos;

        public UserCreditService()
        {
            _clientCreditLimitInfos = new ClientCreditLimitInfo[]
            {
                new ClientCreditLimitInfo
                {
                    IsAcceptableClient = client => client.Name == "VeryImportantClient",
                    HasCreditLimit = false
                },

                new ClientCreditLimitInfo
                {
                    IsAcceptableClient = client => client.Name == "ImportantClient",
                    HasCreditLimit = true,
                    CreditLimitMultiplier = 2f
                },

                new ClientCreditLimitInfo
                {
                    IsAcceptableClient = client => true,
                    HasCreditLimit = true,
                },
            };
        }

        public ClientCreditLimitInfo GetClientCreditLimitInfo(Client client)
        {
            return _clientCreditLimitInfos.First(ccli => ccli.IsAcceptableClient(client));
        }

        public int GetUserCreditLimit(User user)
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}
