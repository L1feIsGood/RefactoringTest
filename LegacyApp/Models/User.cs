using System;

namespace LegacyApp.Models
{
    public class User
    {
        public Client Client { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int CreditLimit { get; set; }
        public bool HasCreditLimit { get; set; }
    }

    // Чтобы не ломать доступ к базе, модели трогать не стал,
    // но я бы запихнул бы CreditLimit и HasCreditLimit в отдельный класс CreditLimitInfo,
    // а его, возможно, в класс в Client
    /*
     *  public class CreditLimitInfo
        {
            public int CreditLimit { get; set; }
            public bool HasCreditLimit { get; set; }
        }
    */
}