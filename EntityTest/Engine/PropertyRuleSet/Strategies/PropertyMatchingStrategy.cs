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
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Conditions;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies
{
    public abstract class PropertyMatchingStrategy : PropertyStrategy
    {
        public List<MatchResult> Validate(PropertyInfo actualProperty, object actualObj, object expectedObj
            , PropertyInfo expectedProperty = null, ParentContext parentContext = null)
        {
            if (StartConditions.Any() &&
                StartConditions.Any(x => x.CanStrategyStart(actualProperty, actualObj, expectedObj, expectedProperty)) ==
                false)
            {
                return new List<MatchResult>();
            }
            return InternalValidate(actualProperty,actualObj,expectedObj,expectedProperty,parentContext);
        }

        protected abstract List<MatchResult> InternalValidate(PropertyInfo actualProperty, object actualObj, object expectedObj
            , PropertyInfo expectedProperty = null, ParentContext parentContext = null);

        public bool StartConditionsMet(PropertyInfo actualProperty, object actualObj, object expectedObj
            , PropertyInfo expectedProperty = null)
        {
            return StartConditions.All(x => x.CanStrategyStart(actualProperty, actualObj, expectedObj, expectedProperty));
        }


        public List<StrategyStartCondition> StartConditions { get; set; } 
    }
}