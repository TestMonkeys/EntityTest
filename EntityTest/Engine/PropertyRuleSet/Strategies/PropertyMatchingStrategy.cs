#region Copyright

// Copyright 2015 Constantin Pascal
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestMonkeys.EntityTest.Engine.HumanReadableMessages;
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Conditions;
using TestMonkeys.EntityTest.Engine.Validators;
using TestMonkeys.EntityTest.Framework;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies
{
    internal abstract class PropertyMatchingStrategy : PropertyStrategy
    {
        public List<StrategyStartCondition> StartConditions { get; set; }
        public string ExpectedPropertyName { get; set; }

        public List<MatchResult> Validate(PropertyInfo actualProperty, object actualObj, object expectedObj,
            ParentContext parentContext = null)
        {
            var expectedProp = GetExpectedProperty(expectedObj, actualProperty);
            if (StartConditions.Any() &&
                StartConditions.Any(x => x.CanStrategyStart(actualProperty, actualObj, expectedObj)) ==
                false)
            {
                return new List<MatchResult>();
            }
            var expected = GetPropertyValue(expectedProp, expectedObj);
            var actual = GetPropertyValue(actualProperty, actualObj);


            if ((expected == null && actual != null) || (expected != null && actual == null))
            {
                var result = new List<MatchResult>();
                var fail = new MatchResult
                {
                    Success = false,
                    Expected = expected ?? SpecialValues.Null,
                    Actual = actual ?? SpecialValues.Null
                };

                if (parentContext != null)
                {
                    fail.Parent = parentContext;
                }
                result.Add(fail);
                return result;
            }
            if (expected == null)
                return new List<MatchResult>();
            if (parentContext != null)
            {
                if (parentContext.IsRecursive(actual))
                    return new List<MatchResult>();
                parentContext.ActualObj = actual;
            }
            return InternalValidate(actualProperty, actualObj, expectedObj, parentContext);
        }

        protected abstract List<MatchResult> InternalValidate(PropertyInfo actualProperty, object actualObj,
            object expectedObj, ParentContext parentContext = null);

        public bool StartConditionsMet(PropertyInfo actualProperty, object actualObj, object expectedObj
            , PropertyInfo expectedProperty = null)
        {
            return StartConditions.All(x => x.CanStrategyStart(actualProperty, actualObj, expectedObj));
        }

        protected PropertyInfo GetExpectedProperty(object expectedObj, PropertyInfo actualProperty)
        {
            if (string.IsNullOrEmpty(ExpectedPropertyName))
                return actualProperty;
            var expectedProperty =
                expectedObj.GetType().GetProperties().FirstOrDefault(x => x.Name.Equals(ExpectedPropertyName));
            if (expectedProperty == null)
                throw new ImproperAttributeUsageException(
                    $"ValidateWithProperty for property <{actualProperty.Name}> was pointing at inexisting <{ExpectedPropertyName}> in type {expectedObj.GetType()}");
            return expectedProperty;
        }
    }
}