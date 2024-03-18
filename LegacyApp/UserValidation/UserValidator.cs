using System;
using System.Collections;
using System.Collections.Generic;
using LegacyApp.Models;

namespace LegacyApp.UserValidation
{
    public class UserValidator : IEnumerable<IUserValidationRule>
    {
        private List<IUserValidationRule> _userValidationRulesList = new List<IUserValidationRule>();

        public UserValidator(params IUserValidationRule[] userValidationRules)
        {
            foreach (var rule in userValidationRules)
            {
                if (rule == null)
                {
                    throw new ArgumentNullException("One of the rules is null!");
                }
            }

            _userValidationRulesList.AddRange(userValidationRules);
        }

        public void Add(IUserValidationRule userValidationRule)
        {
            if (userValidationRule == null)
            {
                throw new ArgumentNullException(nameof(userValidationRule));
            }

            _userValidationRulesList.Add(userValidationRule);
        }

        public bool Validate(User user)
        {
            foreach (var rule in _userValidationRulesList)
            {
                bool userDataIsNotValid = !rule.IsUserDataValid(user);

                if (userDataIsNotValid)
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerator<IUserValidationRule> GetEnumerator()
        {
            foreach (var rule in _userValidationRulesList)
            {
                yield return rule;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
