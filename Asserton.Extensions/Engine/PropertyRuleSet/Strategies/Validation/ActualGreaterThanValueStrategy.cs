using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.Assertion.Extensions.Engine.Validators;
using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;

namespace TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet.Strategies
{
    public class ActualGreaterThanValueStrategy:PropertyValidationStrategy
    {
        private readonly int number;

        public ActualGreaterThanValueStrategy(int number)
        {
            this.number = number;
        }

        public override MatchResult Validate(PropertyInfo propertyInfo, object actualObj, string messagePropertyPrefix = null)
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
                Parent = messagePropertyPrefix,
                PropertyName = propertyInfo.Name
            };
            try{
                var doubleValue = (int)propertyValue;

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
