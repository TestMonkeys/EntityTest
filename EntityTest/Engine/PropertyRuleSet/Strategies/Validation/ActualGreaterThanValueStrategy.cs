using System;
using System.Reflection;
using TestMonkey.EntityTest.Engine.Validators;
using TestMonkey.EntityTest.PropertyAttributes;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies.Validation
{
    public class ActualGreaterThanValueStrategy : PropertyValidationStrategy
    {
        private readonly int number;

        public ActualGreaterThanValueStrategy(int number)
        {
            this.number = number;
        }

        public override MatchResult Validate(PropertyInfo propertyInfo, object actualObj,
            ParentContext messagePropertyPrefix = null)
        {
            var propertyValue = GetPropertyValue(propertyInfo, actualObj);


            if (propertyValue == null)
                throw new ImproperAttributeUsageException(
                    "ValidateActualGreaterThanAttribute should be defined only on numeric properties");
            var result = new MatchResult
            {
                Success = true,
                Expected = "Greater than " + number,
                Actual = propertyValue,
                Parent = new ParentContext(messagePropertyPrefix) {ParentName = propertyInfo.Name}
            };
            try
            {
                var doubleValue = (int) propertyValue;

                //((ValidateActualGreaterThanAttribute)
                // property.GetCustomAttributes(typeof (ValidateActualGreaterThanAttribute), true).First()).Value;
                if (doubleValue <= number)
                    result.Success = false;
            }
            catch (Exception e)
            {
                throw new ImproperAttributeUsageException(e,
                    "ValidateActualGreaterThanAttribute should be defined only on numeric properties");
            }
            return result;
        }
    }
}