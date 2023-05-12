using System;
using System.Security.Policy;

namespace LegacyApp
{
    public class UserService
    {
		public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
		{
			try
			{
				if (!IsUserInputValid(firName, surname, email, dateOfBirth))
				{
					return false;
				}

				var clientRepository = new ClientRepository();
				var client = clientRepository.GetById(clientId);
				var user = CreateUser(client, firName, surname, email, dateOfBirth);
				ValidateAndSetUserCreditLimit(ref user, client);

				UserDataAccess.AddUser(user);

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool IsUserInputValid(string firName, string surname, string email, DateTime dateOfBirth)
		{
			try
			{
				if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname))
					return false;

				if (!email.Contains("@") && !email.Contains("."))
					return false;

				if (GetUserAge(dateOfBirth) < 21)
					return false;

				return true;
			}
			catch (Exception e)
			{
				throw new Exception($"Исключение в UserService.IsUserInputValid {e.Message}");
			}
		}

		public int GetUserAge(DateTime dateOfBirth)
        {
            try
            {
                var now = DateTime.Now;
                int age = now.Year - dateOfBirth.Year;
                DateTime thisYearBirthday = dateOfBirth.AddYears(age);

				if (thisYearBirthday > now)
                    age--;

                return age;
            }
            catch (Exception e)
            {
                throw new Exception($"Исключение в UserService.GetUserAge {e.Message}");
            }
        }

		public User CreateUser(Client client, string firName, string surname, string email, DateTime dateOfBirth)
		{
			try
			{
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
			catch (Exception e)
			{
				throw new Exception($"Исключение в UserService.CreateUser {e.Message}");
			}
		}

		public bool ValidateAndSetUserCreditLimit(ref User user, Client client)
		{
			try
			{
				if (client.Name == "VeryImportantClient")
					// Пропустить проверку лимита
					user.HasCreditLimit = false;
				else if (client.Name == "ImportantClient")
					// Проверить лимит и удвоить его
					SetCreditLimit(ref user, true);
				else
					// Проверить лимит 
					SetCreditLimit(ref user, false);

				var result = user.HasCreditLimit && user.CreditLimit < 500;
				return result;
			}
			catch (Exception e)
			{
				throw new Exception($"Исключение в UserService.ValidateAndSetUserCreditLimit {e.Message}");				
			}
		}

		public void SetCreditLimit(ref User user, bool IncreaseLimitX2)
		{
			try 
			{ 
				user.HasCreditLimit = true;
				using (var userCreditService = new UserCreditServiceClient())
				{
					var creditLimit = userCreditService.GetCreditLimit(
						user.FirstName, user.Surname, user.DateOfBirth);

					if (IncreaseLimitX2) 
						creditLimit *= 2;

					user.CreditLimit = creditLimit;
				}
			}
			catch (Exception e)
			{
				throw new Exception($"Исключение в UserService.SetUserLimit {e.Message}");
			}
		}
    }
}