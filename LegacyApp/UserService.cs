using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IPersonalDataValidationService mPersonalDataValidationService;
        private readonly ICreditLimitsService mCreditLimitsService;


        [Obsolete]
        public UserService()
        {
            //That's not good,
            //Need more DI for the DI god!1 
            mPersonalDataValidationService = PersonalDataValidationServiceClient.Create();
            mCreditLimitsService = CreditLimitsServiceClient.Create();
        }

        public UserService(IPersonalDataValidationService dataValidationService, ICreditLimitsService creditLimitsService)
        {
            mPersonalDataValidationService = dataValidationService;
            mCreditLimitsService = creditLimitsService;
        }


        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var user = CreateUser(firName, surname, email, dateOfBirth, clientId);

            if(!mPersonalDataValidationService.Validate(user))
            {
                return false;
            }

            if(!(mCreditLimitsService.SetCreditLimit(user)
                                     .ValidateCreditLimit(user)))
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }


        //TODO It could be cool to extract this to some user fabric, I guess
        private User CreateUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surname
            };

            return user;
        }
    }
}