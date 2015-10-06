using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Conditions;

namespace TestMonkeys.EntityTest.Framework
{
    public abstract class StrategyConditionAttribute:Attribute
    {
        public abstract StrategyStartCondition StrategyStartCondition { get; }
    }
}
