﻿using TestMonkey.EntityTest.PropertyAttributes;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectIgnoreValidation
    {
        [IgnoreValidationIfDefault]
        public string Value { get; set; }

        [IgnoreValidation]
        public string IgnoredField { get; set; }
    }
}