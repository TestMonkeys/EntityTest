using System;

namespace TestMonkey.EntityTest.PropertyAttributes
{
    /// <summary>
    ///     Will run a full properties validation for decorated List
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ChildPropertySetListAttribute : Attribute
    {
    }
}