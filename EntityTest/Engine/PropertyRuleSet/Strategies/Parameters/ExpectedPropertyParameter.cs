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

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Parameters
{
    internal class ExpectedPropertyParameter : StrategyParameter
    {
        private readonly string propertyName;

        public ExpectedPropertyParameter(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public override void ApplyToStrategy(object strategy)
        {
            var matchingStrategy = strategy as PropertyMatchingStrategy;
            if (matchingStrategy == null)
                throw new Exception($"Could not apply parameter to strategy {strategy.GetType()}");
            matchingStrategy.ExpectedPropertyName = propertyName;
        }
    }
}