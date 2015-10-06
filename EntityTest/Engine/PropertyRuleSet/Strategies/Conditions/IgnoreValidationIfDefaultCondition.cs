using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Validation;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Conditions
{
    public class IgnoreValidationIfDefaultCondition:StrategyStartCondition
    {
        public override bool CanStrategyStart(PropertyInfo actualProperty, object actualObj, object expectedObj,
            PropertyInfo expectedProperty = null)
        {
            var strategy = new IsDefaultValueStrategy();
            return strategy.Validate(expectedProperty ?? actualProperty, expectedObj).Success == false;
        }
    }
}
