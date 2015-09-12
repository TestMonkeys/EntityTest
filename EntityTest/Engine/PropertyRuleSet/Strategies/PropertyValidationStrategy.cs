using System.Reflection;
using TestMonkey.EntityTest.Engine.Validators;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies
{
    public abstract class PropertyValidationStrategy : PropertyStrategy
    {
        public abstract MatchResult Validate(PropertyInfo propertyInfo, object actualObj,
            string messagePropertyPrefix = null);
    }
}