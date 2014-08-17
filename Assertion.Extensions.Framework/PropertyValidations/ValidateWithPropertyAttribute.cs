using System;

namespace TestMonkey.Assertion.Extensions.Framework.PropertyValidations
{
    /// <summary>
    ///     Will validate internalActual property with the defined property from the expected object
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateWithPropertyAttribute : Attribute
    {
        private readonly string propertyName;

        public ValidateWithPropertyAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public String PropertyName
        {
            get { return propertyName; }
        }
    }
}