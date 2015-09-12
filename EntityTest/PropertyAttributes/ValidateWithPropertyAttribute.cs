using System;

namespace TestMonkey.EntityTest.PropertyAttributes
{
    /// <summary>
    ///     Will validate internalActual property with the defined property from the expected object
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateWithPropertyAttribute : Attribute
    {
        public ValidateWithPropertyAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; }
    }
}