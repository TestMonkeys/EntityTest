using System;

namespace TestMonkey.EntityTest.PropertyAttributes
{
    /// <summary>
    ///     Will run a full properties validation for decorated property value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ChildPropertySetAttribute : Attribute
    {
    }
}