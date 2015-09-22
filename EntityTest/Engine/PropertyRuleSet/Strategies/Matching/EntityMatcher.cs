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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestMonkeys.EntityTest.Engine.HumanReadableMessages;
using TestMonkeys.EntityTest.Engine.Validators;
using TestMonkeys.EntityTest.PropertyAttributes;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Matching
{
    public class EntityMatcher : PropertyStrategy
    {
        private readonly ParentContext parentContext;
        private readonly RuleStorage rules = RuleStorage.Instance;
        private List<MatchResult> matchResults;

        public EntityMatcher()
        {
        }

        internal EntityMatcher(ParentContext parentContext)
        {
            this.parentContext = parentContext;
        }

        public List<MatchResult> Compare(object actualObj, object expectedObj, Type byType = null)
        {
            matchResults = new List<MatchResult>();
            ComputeMatch(expectedObj, actualObj, byType, parentContext);


            return matchResults;
        }

        private void ComputeMatch(object expected, object actual, Type byType, ParentContext parent)
        {
            if (NullValidationMatchOrFail(expected, actual, parent))
                return;
            byType = byType ?? expected?.GetType();

            var rule = rules.GetValidationRules(byType);
            var expectedProperties = byType.GetProperties();

            foreach (var property in expectedProperties)
            {
                var propertyName = property.Name;
                ValidateActualConstraints(property, actual, parent, rule);

                if (rule.IgnoreValidationProperties.Contains(property))
                    continue;
                if (!NeedsValidation(property, expected, rule))
                    continue;

                var expectedProperty = GetExpectedProperty(property, expectedProperties, rule);

                var expectedValue = GetPropertyValue(expectedProperty, expected);
                var actualValue = GetPropertyValue(property, actual);

                if (NullValidationMatchOrFail(expectedValue, actualValue,
                    new ParentContext(parent) {ParentName = propertyName}))
                    continue;

                var matchingRule = rule.GetPropertyMatchingStrategy(expectedProperty);
                if (matchingRule != null)
                    matchResults.AddRange(matchingRule.Validate(expectedProperty, actual, expected, property,
                        new ParentContext(parent) {ParentName = property.Name}));
            }
        }

        private bool NullValidationMatchOrFail(object expected, object actual, ParentContext parent)
        {
            if ((expected == null && actual != null) || (expected != null && actual == null))
            {
                ObjectDifferenceFound(expected, actual, parent);
                return true;
            }
            return expected == null;
        }

        private void ValidateActualConstraints(PropertyInfo property, object actual, ParentContext parent,
            ObjectPropertyValidationModel rule)
        {
            var strategy = rule.GetValidationStrategy(property);
            if (strategy == null)
                return;

            var result = strategy.Validate(property, actual, parent);
            if (!result.Success)
                PropertyDifferenceFound(result.Expected, result.Actual, result.Parent); //, result.PropertyName);
        }

        private bool NeedsValidation(PropertyInfo property, object obj, ObjectPropertyValidationModel rule)
        {
            var value = GetPropertyValue(property, obj);

            return !((rule.IgnoreValidationIfDefault.Contains(property) ||
                      rule.ActualNotNullProperties.Contains(property) ||
                      rule.ActualGreaterProperties.ContainsKey(property)) &&
                     IsDefault(value));
        }

        private bool IsDefault(object value)
        {
            if (value == null) return true;
            if (value is int && ((int) value) == 0) return true;
            var potentialString = value as string;
            if (potentialString != null && string.IsNullOrEmpty(potentialString)) return true;
            if (value is DateTime && ((DateTime) value).Equals(DateTime.MinValue)) return true;
            return false;
        }

        private PropertyInfo GetExpectedProperty(PropertyInfo currentProperty, IEnumerable<PropertyInfo> allProperties,
            ObjectPropertyValidationModel rule)
        {
            var expectedProperty = currentProperty;

            if (rule.ValidateActualWithExpectedProperty.ContainsKey(currentProperty))
            {
                expectedProperty = allProperties.FirstOrDefault(
                    x => x.Name.Equals(rule.ValidateActualWithExpectedProperty[currentProperty]));
                if (expectedProperty == null)
                    throw new ImproperAttributeUsageException("ValidateWithProperty for property " +
                                                              currentProperty.Name +
                                                              " was pointing at inexisting property " +
                                                              rule.ValidateActualWithExpectedProperty[
                                                                  currentProperty]);
            }
            return expectedProperty;
        }

        private void ObjectDifferenceFound(object expectedValue, object actualValue, ParentContext parent)
        {
            var result = new MatchResult
            {
                Success = false,
                Expected = expectedValue ?? SpecialValues.Null,
                Actual = actualValue ?? SpecialValues.Null
            };

            if (parent != null)
            {
                result.Parent = parent;
            }
            matchResults.Add(result);
        }

        private void PropertyDifferenceFound(object expectedValue, object actualValue, ParentContext parent)
        {
            var result = new MatchResult
            {
                Success = false,
                Expected = expectedValue ?? SpecialValues.Null,
                Actual = actualValue ?? SpecialValues.Null,
                Parent = parent
            };
            matchResults.Add(result);
        }
    }
}