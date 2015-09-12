using System;
using TestMonkey.EntityTest.Engine.Validators;

namespace TestMonkey.EntityTest
{
    public static class PropertySet
    {
        public static ListOfPropertySetObjectsHelper List { get; } = new ListOfPropertySetObjectsHelper();

        public static PropertySetValidator EqualTo(object expected)
        {
            return new PropertySetValidator(expected);
        }

        public static PropertySetValidator EqualToByInterface(object expected, Type validationType)
        {
            return new PropertySetValidator(expected, validationType);
        }
    }
}