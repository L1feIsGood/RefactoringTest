namespace LegacyApp
{
    public class ClientCreditCounterFactory
    {
        
        public ClientCreditCounterFactory()
        {
            
        }

        public IClientCreditCounter CreateCreditLimitCounter(string clientType)
        {
            if (clientType == "ImportantClient")
                return new ImportantClientCreditCounter();
            else if (clientType == "VeryImportantClient")
                return new VeryImportantClientCreditCounter();
            else
                return new ClientCreditCounter();
        }
    }
}