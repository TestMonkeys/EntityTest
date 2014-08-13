﻿using System;
using TestMonkey.Assertion.Extensions.Framework.Validators;

namespace TestMonkey.Assertion.Extensions
{
    public static class PropertySet
    {
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