using System;

namespace TestMonkey.EntityTest.PropertyAttributes
{
    /// <summary>
    ///     Actual value will be validated to be not null
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateActualNotNullAttribute : Attribute
    {
    }
}