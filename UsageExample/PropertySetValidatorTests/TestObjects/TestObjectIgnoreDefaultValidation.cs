using System;
using TestMonkey.EntityTest.PropertyAttributes;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectIgnoreDefaultValidation
    {
        [IgnoreValidationIfDefault]
        public string StringValue1 { get; set; }

        [IgnoreValidationIfDefault]
        public string StringValue2 { get; set; }

        [IgnoreValidationIfDefault]
        public DateTime DateFiled { get; set; }
    }
}