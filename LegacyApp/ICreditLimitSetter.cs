namespace LegacyApp
{
    // Установщик кредитных лемитов для пользователя.
    // Как можно заметить ниже, отсутствует проверка аргументов на Null.
    // Ее можно вынести в базовый абстрактный класс, где согласно принципу LSP (Liskov substitution principle)
    // сделать пред условия проверки user & creditService на Null, если все ОК, то вызвать абстрактный метод OnSetCreditLimit.
    public interface ICreditLimitSetter
    {
        void SetCreditLimit(User user, IUserCreditService creditService);
    }

    public class ImportantClientCreditLimitSetter : ICreditLimitSetter
    {
        public void SetCreditLimit(User user, IUserCreditService creditService)
        {
            // Проверить лимит и удвоить его
            var creditLimit = creditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);

            user.HasCreditLimit = true;
            user.CreditLimit = creditLimit * 2;
        }
    }

    public class DefaultClientCreditLimitSetter : ICreditLimitSetter
    {
        public void SetCreditLimit(User user, IUserCreditService creditService)
        {
            // Проверить лимит
            var creditLimit = creditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);

            user.HasCreditLimit = true;
            user.CreditLimit = creditLimit;
        }
    }

    public class VeryImportantClientCreditLimitSetter : ICreditLimitSetter
    {
        public void SetCreditLimit(User user, IUserCreditService creditService)
        {
            // Пропустить проверку лимита
            user.HasCreditLimit = false;
        }
    }
}
