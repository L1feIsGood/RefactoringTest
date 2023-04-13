using System;

namespace LegacyApp.Services
{
    internal class UserCreationParams
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClientId { get; set; }
    }
}