using System;
using System.Reflection;
using TestMonkey.EntityTest.PropertyAttributes;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies
{
    public abstract class PropertyStrategy
    {
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