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
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Matching;
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Parameters;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Builders
{
    public class EntityListMatcherStrategyBuilder : IMatchingStrategyBuilder
    {
        private List<StrategyParameter> parameters = new List<StrategyParameter>();
        public ItemsMatch Values { get; set; }
        public OrderMatch Order { get; set; }

        public override PropertyMatchingStrategy GetStrategy()
        {
            var strategy = new ChildEntityListMatchingStrategy();
            foreach (var param in parameters)
                param.ApplyToStrategy(strategy);
            strategy.StartConditions = this.strategyStartConditions;
            return strategy;
        }

        public override void AddParameters(List<StrategyParameter> parameters)
        {
            this.parameters = parameters;
        }
    }
}