using System;

namespace TestMonkey.Assertion.Extensions.Framework.PropertyValidations
{
    /// <summary>
    ///     Will run a full properties validation for decorated property value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ChildPropertySetAttribute : Attribute
    {
    }
}