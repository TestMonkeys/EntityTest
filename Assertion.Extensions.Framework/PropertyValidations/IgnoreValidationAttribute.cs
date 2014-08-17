using System;

namespace TestMonkey.Assertion.Extensions.Framework.PropertyValidations
{
    /// <summary>
    ///     Will ignore validation for decorated property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreValidationAttribute : Attribute
    {
    }
}