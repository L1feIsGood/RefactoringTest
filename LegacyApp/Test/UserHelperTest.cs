namespace LegacyApp.Test
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class UserHelperTest
    {
        private readonly IUserHelper _userHelper;

        public UserHelperTest()
        {
            _userHelper = new UserHelper();
        }
        [Test]
        public void IsStringEmptyOrNullTest()
        {
            string nullString = null;
            var emptyString = "";
            var someString = "test String@2 ";

            Assert.That(_userHelper.IsStringEmptyOrNull(nullString), Is.True);
            Assert.That(_userHelper.IsStringEmptyOrNull(emptyString), Is.True);
            Assert.That(_userHelper.IsStringEmptyOrNull(someString), Is.False);
        }
        [Test]
        public void IsEmailCorrect()
        {
            var correctEmail = "kest3107@gmail.com";
            var wrongEmail = "kest3107gmailcom";

            Assert.That(_userHelper.IsEmailCorrect(correctEmail), Is.True);
            Assert.That(_userHelper.IsEmailCorrect(wrongEmail), Is.False);
        }
        [Test]
        public void IsAgeNotCorrect()
        {
            var correctAge = new DateTime(1995, 5, 25);
            var wrongAge = new DateTime(2005, 12, 25);

            Assert.That(_userHelper.IsAgeCorrect(correctAge), Is.True);
            Assert.That(_userHelper.IsAgeCorrect(wrongAge), Is.False);
        }
        [Test]
        public void IsCreditLimitCorrect()
        {
            var hasCreditLimit = true;
            var hasntCreditLimit = false;
            var creditLimit = 500;

            Assert.That(_userHelper.IsCreditLimitCorrect(hasCreditLimit, creditLimit + 1), Is.True);
            Assert.That(_userHelper.IsCreditLimitCorrect(hasntCreditLimit, creditLimit + 1), Is.True);
            Assert.That(_userHelper.IsCreditLimitCorrect(hasCreditLimit, creditLimit - 1), Is.False);
            Assert.That(_userHelper.IsCreditLimitCorrect(hasntCreditLimit, creditLimit - 1), Is.True);
        }
    }
}
