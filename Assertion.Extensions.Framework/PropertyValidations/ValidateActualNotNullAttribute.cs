using System;

namespace TestMonkey.Assertion.Extensions.Framework.PropertyValidations
{
    /// <summary>
    ///     Actual value will be validated to be not null
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateActualNotNullAttribute : Attribute
    {
    }
}