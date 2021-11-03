using System.Collections.Generic;

namespace LegacyApp
{
    public class ClientCreditCounterFactory
    {
        private readonly IEnumerable<IClientCreditCounter> _clientCreditCounters;
        public ClientCreditCounterFactory()
        {
            _clientCreditCounters = new List<IClientCreditCounter>
            {
                new ClientCreditCounter(),
                new ImportantClientCreditCounter(),
                new VeryImportantClientCreditCounter()
            };
        }

        public IClientCreditCounter CreateCreditLimitCounter(string clientType)
        {
            foreach (var creditLimitCounter in _clientCreditCounters)
            {
                if (clientType == creditLimitCounter.ClientType)
                    return creditLimitCounter;
            }
            return new ClientCreditCounter();
        }
    }
}