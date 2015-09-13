using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using TestMonkey.EntityTest.Engine.Validators;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies.Matching
{
    internal class DefaultMatchingSrategy : PropertyMatchingStrategy
    {
        public override List<MatchResult> Validate(PropertyInfo expectedProperty, object actualObj, object expectedObj,
            PropertyInfo actualProperty = null,
            ParentContext messagePropertyPrefix = null)
        {
            var result = new List<MatchResult>();
            var expectedValue = GetPropertyValue(expectedProperty, expectedObj);
            var actualValue = GetPropertyValue(actualProperty??expectedProperty, actualObj);
            if (!expectedValue.Equals(actualValue))
            {
                result.Add(new MatchResult
                {
                    Success = false,
                    Expected = expectedValue,
                    Actual = actualValue,
                    Parent = messagePropertyPrefix//,
                    //PropertyName = expectedProperty.Name
                });
            }
            return result;
        }
    }
}
