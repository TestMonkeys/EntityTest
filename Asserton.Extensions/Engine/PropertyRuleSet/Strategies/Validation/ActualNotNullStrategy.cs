using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.Assertion.Extensions.Engine.HumanReadableMessages;
using TestMonkey.Assertion.Extensions.Engine.Validators;

namespace TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet.Strategies
{
    public class ActualNotNullStrategy:PropertyValidationStrategy
    {
        public override MatchResult Validate(PropertyInfo propertyInfo, object actualObj, string messagePropertyPrefix = null)
        {
            if (GetPropertyValue(propertyInfo, actualObj) == null)
                return new MatchResult
                {
                    Success = false,
                    Actual = SpecialValues.Null,
                    Expected = SpecialValues.NotNull,
                    Parent = messagePropertyPrefix,
                    PropertyName = propertyInfo.Name
                };
            return new MatchResult {Success = true};
        }
    }
}
