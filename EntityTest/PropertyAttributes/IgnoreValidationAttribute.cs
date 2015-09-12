using System;

namespace TestMonkey.EntityTest.PropertyAttributes
{
    /// <summary>
    ///     Will ignore validation for decorated property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreValidationAttribute : Attribute
    {
    }
}