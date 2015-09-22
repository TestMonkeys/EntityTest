using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Builders
{
    public interface IValidationStrategyBuilder
    {
        PropertyValidationStrategy GetStrategy();
    }
}
