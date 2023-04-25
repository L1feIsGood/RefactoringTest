using FluentAssertions;
using LegacyApp;
using NUnit.Framework;

namespace UserServiceShould
{
    /*
     В этом классе представлены базовые тесты на корректный ввод.
     В идеале нужно мокнуть ClientRepository и протестировать
     поведение метода AddUser() при работе с разными клиентами.
    */

    [TestFixture]
    public class UserServiceShould
    {
        private UserService service;
        private DateTime currentDate;

        [SetUp]
        public void SetUp()
        {
            service = new UserService();
            currentDate = DateTime.Now;
        }

        [TestCase(null, "lastName")]
        [TestCase("", "lastName")]
        [TestCase("firstName", null)]
        [TestCase("firstName", "")]
        public void AddUser_NameNullOrEmpty_ShouldReturnFalse(string firstName, string lastName)
        {
            var addResult = service.AddUser(firstName, 
                lastName, 
                "email@email.com", 
                DateTime.MinValue, 
                1);

            addResult.Should().BeFalse();
        }

        [TestCase("email.email.com")]
        [TestCase("email@email")]
        [TestCase("email")]
        [TestCase("")]
        [TestCase(null)]
        public void AddUser_EmailInvalid_ShouldReturnFalse(string email)
        {
            var addResult = service.AddUser("firstName",
                "lastName",
                email,
                DateTime.MinValue,
                1);

            addResult.Should().BeFalse();
        }

        [Test]
        public void AddUser_AgeLessThan21_ShouldReturnFalse()
        {
            var checkDate = currentDate.Subtract(new TimeSpan(365, 0, 0, 0));

            var addResult = service.AddUser("firstName",
                "lastName",
                "email",
                currentDate,
                1);

            addResult.Should().BeFalse();
        }
    }
}