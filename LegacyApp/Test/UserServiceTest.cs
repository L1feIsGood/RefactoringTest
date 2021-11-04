namespace LegacyApp.Test
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class UserServiceTest
    {
        private readonly UserService _userService;
        public UserServiceTest()
        {
            _userService = new UserService();
        }
        [Test]
        public void AddUser()
        {
            var user1 = _userService.AddUser("Владислав", "Бочкарев", "kemsikov@bk.ru", new DateTime(2003, 10, 1), 18);
            var user2 = _userService.AddUser("", "Сумароков", "kest3107@gmail.com", new DateTime(1997, 7, 31), 18);
            var user3 = _userService.AddUser("Константин", "", "kest3107@gmail.com", new DateTime(1997, 7, 31), 18);
            var user4 = _userService.AddUser("Константин", "Сумароков", "kest3107gmailcom", new DateTime(1997, 7, 31), 18);
            Assert.That(user1, Is.False);
            Assert.That(user2, Is.False);
            Assert.That(user3, Is.False);
            Assert.That(user4, Is.False);
        }
    }
}
