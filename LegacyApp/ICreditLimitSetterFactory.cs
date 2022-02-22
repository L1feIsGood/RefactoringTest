using System;

namespace LegacyApp
{
    // Позволяет гибко создавать стратегию установки кридитного лимита клиентам.
    public interface ICreditLimitSetterFactory
    {
        ICreditLimitSetter Create(User user);
    }

    public class CreditLimitSetterFactory : ICreditLimitSetterFactory
    {
        private const string VeryImportantClient = "VeryImportantClient";
        private const string ImportantClient = "ImportantClient";

        public ICreditLimitSetter Create(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (user.Client == null)
                throw new NullReferenceException("The client must not be Null.");

            if (user.Client.Name == VeryImportantClient)
                return new VeryImportantClientCreditLimitSetter();

            if (user.Client.Name == ImportantClient)
                return new ImportantClientCreditLimitSetter();

            return new DefaultClientCreditLimitSetter();
        }
    }
}
