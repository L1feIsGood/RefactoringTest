namespace LegacyApp
{
    public class CreditLimitsServiceClient : ICreditLimitsService
    {
        private readonly int mDefaultCreditMultiplier = 1;
        private readonly int mMinCreditLimit = 500;


        public static ICreditLimitsService Create()
        {
            return new CreditLimitsServiceClient();
        }

        public ICreditLimitsService SetCreditLimit(User user)
        {
            var creditMultiplier = mDefaultCreditMultiplier;

            switch(user.Client.Name)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClilent":
                    creditMultiplier = 2;
                    break;
                default:
                    creditMultiplier = mDefaultCreditMultiplier;
                    break;
            }

            if(user.HasCreditLimit)
            {
                using(var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    user.CreditLimit = creditLimit * creditMultiplier;
                }
            }

            return this;
        }

        public bool ValidateCreditLimit(User user)
        {
            return !user.HasCreditLimit || user.CreditLimit >= mMinCreditLimit;
        }
    }
}
