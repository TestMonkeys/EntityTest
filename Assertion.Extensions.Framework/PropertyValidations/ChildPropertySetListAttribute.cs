using System;

namespace TestMonkey.Assertion.Extensions.Framework.PropertyValidations
{
    /// <summary>
    ///     Will run a full properties validation for decorated List
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ChildPropertySetListAttribute : Attribute
    {
    }
}