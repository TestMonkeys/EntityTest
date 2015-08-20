using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.Assertion.Extensions.Engine.Validators;
using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;

namespace TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet
{
    public abstract class PropertyValidationStrategy
    {
        public abstract ValidationResult Validate(PropertyInfo propertyInfo, object actualObj,
            string messagePropertyPrefix = null);


        protected object GetPropertyValue(PropertyInfo property, object obj)
        {
            object value;
            try
            {
                value = property.GetValue(obj, null);
            }
            catch (Exception e)
            {
                throw new ImproperTypeUsageException(e, "Could not get property {0} from object of type {1}",
                    property.Name, obj.GetType().FullName);
            }

            return value;
        }
    }
}
