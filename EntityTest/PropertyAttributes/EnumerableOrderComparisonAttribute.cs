using System;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EnumerableOrderComparisonAttribute : Attribute
    {
        internal OrderMatch Option;

        public EnumerableOrderComparisonAttribute(OrderMatch option)
        {
            Option = option;
        }
    }
}