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
using System.Reflection;
using TestMonkeys.EntityTest.Engine.HumanReadableMessages;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Matching
{
    public class EntityMatchingStrategy : PropertyStrategy
    {
        private readonly ParentContext parentContext;
        private readonly RuleStorage rules = RuleStorage.Instance;
        private List<MatchResult> matchResults;

        public EntityMatchingStrategy()
        {
        }

        internal EntityMatchingStrategy(ParentContext parentContext)
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
                ValidateActualConstraints(property, actual, parent, rule);

                if (rule.IgnoreValidationProperties.Contains(property))
                    continue;

                var matchingRule = rule.GetPropertyMatchingStrategy(property);
                if (matchingRule != null)
                    matchResults.AddRange(matchingRule.Validate(property, actual, expected,
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