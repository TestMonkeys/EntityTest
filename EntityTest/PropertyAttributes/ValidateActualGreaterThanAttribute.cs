using System;

namespace TestMonkey.EntityTest.PropertyAttributes
{
    /// <summary>
    ///     Actual value will be validated to be greater than desired value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateActualGreaterThanAttribute : Attribute
    {
        public ValidateActualGreaterThanAttribute(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}