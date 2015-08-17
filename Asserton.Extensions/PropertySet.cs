using System;
using TestMonkey.Assertion.Extensions.Engine.Validators;

namespace TestMonkey.Assertion.Extensions
{
    public static class PropertySet
    {
        private static ListOfPropertySetObjectsHelper listHelper = new ListOfPropertySetObjectsHelper();

        public static PropertySetValidator EqualTo(object expected)
        {
            return new PropertySetValidator(expected);
        }

        public static PropertySetValidator EqualToByInterface(object expected, Type validationType)
        {
            return new PropertySetValidator(expected, validationType);
        }

        public static ListOfPropertySetObjectsHelper List
        {
            get { return listHelper; }
        }
    }
}