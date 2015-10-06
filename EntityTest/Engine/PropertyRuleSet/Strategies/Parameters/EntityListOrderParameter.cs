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
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Matching;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Parameters
{
    public class EntityListOrderParameter : StrategyParameter
    {
        private readonly OrderMatch order;

        public EntityListOrderParameter(OrderMatch order)
        {
            this.order = order;
        }

        public override void ApplyToStrategy(object strategy)
        {
            if (strategy == null)
                throw new ArgumentException("Strategy can not be null", nameof(strategy));

            var entityChildListStrategy = strategy as ChildEntityListMatchingStrategy;
            if (entityChildListStrategy != null)
            {
                entityChildListStrategy.PositionComparison = order;
                return;
            }
            var entityListStrategy = strategy as EntityListMatchingStrategy;
            if (entityListStrategy != null)
            {
                entityListStrategy.PositionComparison = order;
                return;
            }
            throw new InvalidOperationException(
                $"Can not apply EntityListOrder verfication parameter on strategy: {strategy.GetType()}");
        }
    }
}