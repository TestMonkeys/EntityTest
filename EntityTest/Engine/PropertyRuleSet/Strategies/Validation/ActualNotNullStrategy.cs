using System.Reflection;
using TestMonkey.EntityTest.Engine.HumanReadableMessages;
using TestMonkey.EntityTest.Engine.Validators;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies.Validation
{
    public class ActualNotNullStrategy : PropertyValidationStrategy
    {
        public override MatchResult Validate(PropertyInfo propertyInfo, object actualObj,
            ParentContext messagePropertyPrefix = null)
        {
            if (GetPropertyValue(propertyInfo, actualObj) == null)
                return new MatchResult
                {
                    Success = false,
                    Actual = SpecialValues.Null,
                    Expected = SpecialValues.NotNull,
                    Parent = new ParentContext(messagePropertyPrefix) {ParentName = propertyInfo.Name}
                };
            return new MatchResult {Success = true};
        }
    }
}