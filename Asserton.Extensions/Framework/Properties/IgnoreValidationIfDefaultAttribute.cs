using System;

namespace TestMonkey.Assertion.Extensions.Framework.Properties
{
    /// <summary>
    ///     Will ignore validation if expected value is default
    ///     Default values are StringNullOrEmpty, DateTime.MinValue and null
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreValidationIfDefaultAttribute : Attribute
    {
    }
}