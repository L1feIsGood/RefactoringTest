using System.Collections.Generic;

namespace LegacyApp
{
    class ClientRoleGenerate
    {
        private readonly IEnumerable<IClientRole> _clientRole;
        public ClientRoleGenerate()
        {
            _clientRole = new List<IClientRole>
            {
                new ClientRoleDefault(),
                new ClientRoleImportant(),
                new ClientRoleVeryImportant()
            };
        }
        public IClientRole CreateClientRole(string clientType)
        {
            foreach (var client in _clientRole)
            {
                if (clientType == client.ClientType)
                    return client;
            }
            return new ClientRoleDefault();
        }
    }
}
