using System;

namespace TestMonkey.Assertion.Extensions.Framework.Properties
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

        internal int Value { get; set; }
    }
}