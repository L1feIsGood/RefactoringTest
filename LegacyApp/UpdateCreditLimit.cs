namespace LegacyApp;

public interface IUpdateCreditLimit
{
    void Update(User user);
}

public class UpdateCreditLimit : IUpdateCreditLimit
{
    public void Update(User user)
    {
        int limitMultiplier = 1;
        switch (user.Client.Name)
        {
            // Пропустить проверку лимита
            case "VeryImportantClient":
                user.HasCreditLimit = false;
                return;
            // Проверить лимит и удвоить его
            case "ImportantClient":
                user.HasCreditLimit = true;
                limitMultiplier = 2;
                break;
            // Проверить лимит
            default:
                user.HasCreditLimit = true;
                break;
        }

        using var userCreditService = new UserCreditServiceClient();

        int creditLimit = userCreditService.
            GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);

        creditLimit *= limitMultiplier;
        user.CreditLimit = creditLimit;

        userCreditService.Dispose();
    }
}

