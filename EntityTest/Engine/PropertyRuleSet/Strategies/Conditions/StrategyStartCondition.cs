using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Conditions
{
    public abstract class  StrategyStartCondition
    {
     public abstract bool CanStrategyStart(PropertyInfo actualProperty, object actualObj, object expectedObj
            , PropertyInfo expectedProperty = null);
    }
}
