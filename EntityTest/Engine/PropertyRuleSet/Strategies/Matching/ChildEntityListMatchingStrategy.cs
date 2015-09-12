using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.EntityTest.Engine.Validators;
using TestMonkey.EntityTest.PropertyAttributes;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies.Matching
{
    class ChildEntityListMatchingStrategy:PropertyMatchingStrategy
    {
        public override List<MatchResult> Validate(PropertyInfo expectedProperty, object actualObj, object expectedObj, PropertyInfo actualProperty = null,
            string messagePropertyPrefix = null)
        {
            var expectedValue = GetPropertyValue(expectedProperty, expectedObj);
            if (!(expectedValue is IList))
                throw new ImproperAttributeUsageException("Expected property " + expectedProperty.Name + " is not an instance of IList");

            var actualValue = GetPropertyValue(expectedProperty, actualObj);
            var listMatcher = new EntityListMatcher(messagePropertyPrefix);
            var entityListResult = listMatcher.Compare((IList) actualValue, (IList) expectedValue);
            var resultList = entityListResult.ListMatchResults;
            resultList.AddRange(entityListResult.EntityMatchResults.SelectMany(x => x.MemberResults).ToList());
            return resultList;
        }
    }
}
