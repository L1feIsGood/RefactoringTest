using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IValidateNewUser validateNewUser;
        private readonly IUpdateCreditLimit updateCreditLimit;
        private readonly ClientRepository clientRepository;

        public UserService(IValidateNewUser validateNewUser, IUpdateCreditLimit updateCreditLimit)
        {
            this.validateNewUser = validateNewUser;
            this.updateCreditLimit = updateCreditLimit;

            clientRepository = new();
        }

        public UserService() : this(
            new ValidateNewUser(),
            new UpdateCreditLimit())
        { }

        public bool AddUser(string firName,
            string surName,
            string email,
            DateTime dateOfBirth,
            int clientId)
        {
            if (!validateNewUser.Validate(firName, surName, email, dateOfBirth))
                return false;


            var user = new User
            {
                Client = clientRepository.GetById(clientId),
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firName,
                Surname = surName
            };

            updateCreditLimit.Update(user);

            if (user.HasCreditLimit && user.CreditLimit < 500)
                return false;

            UserDataAccess.AddUser(user);

            return true;
        }
    }
}