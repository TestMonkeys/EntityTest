using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.EntityTest.Engine.Validators;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies.Matching
{
    class ChildEnitityMatchingStrategy:PropertyMatchingStrategy
    {
        public override List<MatchResult> Validate(PropertyInfo expectedProperty, object actualObj, object expectedObj,
            PropertyInfo actualProperty=null, string messagePropertyPrefix = null)
        {
            var actualChild = GetPropertyValue(expectedProperty, actualObj);
            var expectedChild = GetPropertyValue(actualProperty??expectedProperty, expectedObj);
            var matcher = new EntityMatcher(messagePropertyPrefix);
            return matcher.Compare(actualChild, expectedChild);
        }
    }
}
