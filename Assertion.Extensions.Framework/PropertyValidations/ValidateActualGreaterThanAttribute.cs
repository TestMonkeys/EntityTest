using System;

namespace TestMonkey.Assertion.Extensions.Framework.PropertyValidations
{
    /// <summary>
    ///     Actual value will be validated to be greater than desired value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateActualGreaterThanAttribute : Attribute
    {
        public ValidateActualGreaterThanAttribute(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}