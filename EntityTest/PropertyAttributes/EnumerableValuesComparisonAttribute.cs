using System;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EnumerableValuesComparisonAttribute : Attribute
    {
        public EnumerableValuesComparisonAttribute(ItemsMatch option)
        {
            Option = option;
        }

        internal ItemsMatch Option { get; }
    }
}