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

using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Matching;
using TestMonkeys.EntityTest.Engine.Validators;
using TestMonkeys.EntityTest.Framework;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Builders
{
    public class EntityListMatcherStrategyBuilder : IMatchingStrategyBuilder
    {
        public ItemsMatch Values { get; set; }
        public OrderMatch Order { get; set; }

        public PropertyMatchingStrategy GetStrategy()
        {
            var strategy = new ChildEntityListMatchingStrategy
            {
                PositionComparison = Order,
                ValuesComparison = Values
            };
            return strategy;
        }

        public void ApplyConstraints(object[] attributes)
        {
            foreach (var attribute in attributes)
            {
                var comparisonAttribute = attribute as EnumerableOrderComparisonAttribute;
                if (comparisonAttribute != null)
                    Order = comparisonAttribute.Option;

                var valuesComparisonAttribute = attribute as EnumerableValuesComparisonAttribute;
                if (valuesComparisonAttribute != null)
                    Values = valuesComparisonAttribute.Option;
            }
        }
    }
}