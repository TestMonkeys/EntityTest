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

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Validation
{
    internal class EntityValidationStrategy : PropertyStrategy
    {
        private readonly ParentContext parentContext;
        private readonly RuleStorage rules = RuleStorage.Instance;
        private List<MatchResult> matchResults;

        public EntityValidationStrategy()
        {
        }

        internal EntityValidationStrategy(ParentContext parentContext)
        {
            this.parentContext = parentContext;
        }

        public List<MatchResult> Validate(object actual, Type validationType)
        {
            matchResults = new List<MatchResult>();
            ComputeMatch(actual, validationType, parentContext);


            return matchResults;
        }


        private void ComputeMatch(object actual, Type byType, ParentContext parent)
        {
            byType = byType ?? actual?.GetType();

            var rule = rules.GetValidationRules(byType);
            var expectedProperties = byType.GetProperties();

            foreach (var property in expectedProperties)
            {
                ValidateActualConstraints(property, actual, parent, rule);
            }

            foreach (var property in rule.ChildRelations)
            {
                var child = GetPropertyValue(property, actual);
                var propParent = new ParentContext(parent)
                {
                    ParentName = property.Name,
                    ActualObj = actual
                };
                if (propParent.IsRecursive(actual))
                    continue;
                ComputeMatch(child, child.GetType(), propParent);
            }
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