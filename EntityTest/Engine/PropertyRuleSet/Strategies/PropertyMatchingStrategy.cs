using System.Collections.Generic;
using System.Reflection;
using TestMonkey.EntityTest.Engine.Validators;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies
{
    public abstract class PropertyMatchingStrategy : PropertyStrategy
    {
        public abstract List<MatchResult> Validate(PropertyInfo expectedProperty, object actualObj, object expectedObj
            , PropertyInfo actualProperty = null, ParentContext messagePropertyPrefix = null);
    }
}