using System;
using TestMonkey.Assertion.Extensions.Framework.Properties;

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