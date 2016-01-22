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
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Conditions;
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Parameters;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Builders
{
    internal abstract class MatchingStrategyBuilder
    {
        protected List<StrategyParameter> strategyParameters;
        protected List<StrategyStartCondition> strategyStartConditions;
        public abstract PropertyMatchingStrategy GetStrategy();

        public void AddParameters(List<StrategyParameter> parameters)
        {
            strategyParameters = parameters;
        }

        public void AddConditions(List<StrategyStartCondition> strategyStartConditions)
        {
            this.strategyStartConditions = strategyStartConditions;
        }

        public void ApplyModifiers(PropertyMatchingStrategy strategy)
        {
            strategy.StartConditions = strategyStartConditions;
            foreach (var param in strategyParameters)
                param.ApplyToStrategy(strategy);
        }
    }
}