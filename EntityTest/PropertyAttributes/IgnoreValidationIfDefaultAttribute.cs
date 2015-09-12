using System;

namespace TestMonkey.EntityTest.PropertyAttributes
{
    /// <summary>
    ///     Will ignore validation if expected value is default
    ///     Default values are StringNullOrEmpty, DateTime.MinValue and null
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreValidationIfDefaultAttribute : Attribute
    {
    }
}