using System;

namespace LegacyApp.Models
{
    public class ClientCreditLimitInfo
    {
        public Func<Client, bool> IsAcceptableClient { get; set; }
        public bool HasCreditLimit { get; set; } = true;
        public float CreditLimitMultiplier { get; set; } = 1f;
    }
}
